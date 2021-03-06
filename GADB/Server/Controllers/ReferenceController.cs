using AutoMapper;
using GADB.Server.Models.DB;
using GADB.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GADB.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReferenceController : ControllerBase
    {

        public readonly GADBContext _context;
        public readonly IMapper _mapper;

        public ReferenceController(GADBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/<ReferenceController>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await _context.TreferenceValue.ToListAsync();
            return Ok(_mapper.Map<IList<ReferenceValue>>(list));
        }

        // POST api/<ReferenceController>
        [HttpPost]
        public async Task<IActionResult> Post(ReferenceValue item)
        {
            var tItem = _mapper.Map<TreferenceValue>(item);
            var checkUniqueName = await _context.TreferenceValue.FirstOrDefaultAsync(d => d.Name.Equals(tItem.Name));

            if (checkUniqueName == null)
            {
                

                if (tItem.Id == Guid.Empty)
                {
                    // create new item
                    tItem.Id = Guid.NewGuid();
                    _context.TreferenceValue.Add(tItem);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    _context.Update(tItem);
                    await _context.SaveChangesAsync();
                }

                return await GetAll();
            }
            else
            {
                return BadRequest("Name already used. Please choose different list name.");
            }


        }

        // DELETE api/<ReferenceController>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteByID(Guid id)
        {
            try
            {
                var itemRef = await _context.TreferenceValue.FindAsync(id);
                _context.TreferenceValue.Remove(itemRef);
                await _context.SaveChangesAsync();
                return Ok("Value list " + itemRef.Name + " deleted.");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
