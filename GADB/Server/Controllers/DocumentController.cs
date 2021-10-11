using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using GADB.Server.Models.DB;
using GADB.Shared.Models;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GADB.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {

        public readonly GADBContext _context;
        public readonly IMapper _mapper;

        public DocumentController(GADBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/<DocumentController>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var tList = await _context.Tdocumenttype.ToListAsync();
            var list = _mapper.Map<IList<Documenttype>>(tList);

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

        // POST api/<DocumentController>
        [HttpPost]
        public async Task<IActionResult> Post(Documenttype item)
        {
            var tItem = _mapper.Map<Tdocumenttype>(item);
            if (tItem.Id == Guid.Empty)
            {
                // new item
                tItem.Id = Guid.NewGuid();
                _context.Tdocumenttype.Add(tItem);
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
                var itemDoc = await _context.Tdocumenttype.FindAsync(id);
                _context.Tdocumenttype.Remove(itemDoc);
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
