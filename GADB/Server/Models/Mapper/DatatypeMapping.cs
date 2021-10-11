using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GADB.Server.Models.DB;
using GADB.Shared.Models;

namespace GADB.Server.Models.Mapper
{
    public class DatatypeMapping:Profile
    {
        public DatatypeMapping()
        {
            CreateMap<Tdatatype, Datatype>().ReverseMap();
        }
    }
}
