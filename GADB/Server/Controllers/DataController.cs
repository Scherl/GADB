using AutoMapper;
using GADB.Server.Interfaces;
using GADB.Server.Models.DB;
using GADB.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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
            var list = await _dataService.GetAll();
            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {


            var item = await _dataService.GetById(id);
            return Ok(item);
        }

        //Get subsidiaries
        [HttpGet("subs/{id}")]
        public async Task<IActionResult> GetSubsidiaries(Guid id)
        {
            var list = await _dataService.GetSubsidiaries(id);
            return Ok(list);
        }

        //Get HQ by id 
        [HttpGet("main/{id}")]
        public async Task<IActionResult> GetHQ(Guid id)
        {
            try
            {
                var ret = await _dataService.GetHQ(id);
                return Ok(ret);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
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
