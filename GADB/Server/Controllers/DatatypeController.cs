using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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
    public class DatatypeController : ControllerBase
    {

        public readonly GADBContext _context;
        public readonly IMapper _mapper;

        public DatatypeController(GADBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/<DatatypeController>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await _context.Tdatatype.ToListAsync();
            return Ok(_mapper.Map<IList<Datatype>>(list));
        }

    }
}
