using AutoMapper;
using GADB.Server.Interfaces;
using GADB.Server.Models.DB;
using GADB.Shared.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace GADB.Server.Services
{
    public class DataService : IDataService
    {
        private readonly GADBContext _context;
        private readonly IMapper _mapper;

        public DataService(GADBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }

     
       
        //TODO: Insert Data Service 
        



    }
}
