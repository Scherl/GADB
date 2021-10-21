using AutoMapper;
using GADB.Server.Interfaces;
using GADB.Server.Models.DB;
using GADB.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GADB.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        public readonly GADBContext _context;
        public readonly IMapper _mapper;
        public readonly IDataService _dataService;

        public DataController(GADBContext context, IMapper mapper, IDataService dataService)
        {
            _context = context;
            _mapper = mapper;
            _dataService = dataService;
        }

        // GET: api/<DataController>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var onlyHeadquarter = "\"Name\":\"Ist Hauptadresse\",\"Datatype\":\"Boolean\",\"ReferenceId\":null,\"Value\":\"True\"";
            var tList = await _context.Tdata.Where(d => d.Elements.Contains(onlyHeadquarter)).Include(x => x.Doc).ToListAsync();
            var list = _mapper.Map<IList<Data>>(tList);

            foreach (var entry in list)
            {
                if (!string.IsNullOrEmpty(entry.Elements))
                {
                    entry.DataElements =
                        new List<DataElement>(JsonSerializer.Deserialize<IList<DataElement>>(entry.Elements));
                }
            }

            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {


            var tItem = await _context.Tdata.FindAsync(id);
            var item = _mapper.Map<Data>(tItem);
            item.DataElements = new List<DataElement>(JsonSerializer.Deserialize<IList<DataElement>>(item.Elements));

            return Ok(item);
        }

        //Get subsidiaries
        [HttpGet("subs/{id}")]
        public async Task<IActionResult> GetSubsidiaries(Guid id)
        {
            var tItem = await _context.Tdata.FindAsync(id);
            var item = _mapper.Map<Data>(tItem);
            item.DataElements = new List<DataElement>(JsonSerializer.Deserialize<IList<DataElement>>(item.Elements));
            var name = item.DataElements.Single(n => n.Name == "Name");
            var isNotHQ = "\"Name\":\"Ist Hauptadresse\",\"Datatype\":\"Boolean\",\"ReferenceId\":null,\"Value\":\"False\"";

            var tList = await _context.Tdata.Where(d => d.Elements.Contains(name.Value) && d.Elements.Contains(isNotHQ) && d.Id != id).Include(x => x.Doc).ToListAsync();
            var list = _mapper.Map<IList<Data>>(tList);

            foreach (var entry in list)
            {
                if (!string.IsNullOrEmpty(entry.Elements))
                {
                    entry.DataElements =
                        new List<DataElement>(JsonSerializer.Deserialize<IList<DataElement>>(entry.Elements));
                }
            }

            return Ok(list);
        }

        //Get HQ by id 
        [HttpGet("main/{id}")]
        public async Task<IActionResult> GetHQ(Guid id)
        {
            var tItem = await _context.Tdata.FindAsync(id);
            var item = _mapper.Map<Data>(tItem);
            item.DataElements = new List<DataElement>(JsonSerializer.Deserialize<IList<DataElement>>(item.Elements));
            var name = item.DataElements.Single(n => n.Name == "Name");
            var isHQ = "\"Name\":\"Ist Hauptadresse\",\"Datatype\":\"Boolean\",\"ReferenceId\":null,\"Value\":\"True\"";

            var tItemData = await _context.Tdata.Include(x => x.Doc).SingleAsync(d => d.Elements.Contains(name.Value) && d.Elements.Contains(isHQ) && d.Id != id);
            var itemData = _mapper.Map<Data>(tItemData);

            if (!string.IsNullOrEmpty(itemData.Elements))
            {
                itemData.DataElements =
                    new List<DataElement>(JsonSerializer.Deserialize<IList<DataElement>>(itemData.Elements));
            }

            return Ok(itemData);
        }

        // POST api/<DataController>
        [HttpPost]
        public async Task<IActionResult> Post(Data data)
        {
            var tItem = _mapper.Map<Tdata>(data);
            if (tItem.Id == Guid.Empty)
            {
                // new item
                tItem.Id = Guid.NewGuid();
                _context.Tdata.Add(tItem);
                await _context.SaveChangesAsync();
            }
            else
            {
                _context.Update(tItem);
                await _context.SaveChangesAsync();
            }

            return await GetAll();
        }

        //DELETE api/<DocumentController>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var itemDoc = await _context.Tdata.FindAsync(id);
                _context.Tdata.Remove(itemDoc);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
