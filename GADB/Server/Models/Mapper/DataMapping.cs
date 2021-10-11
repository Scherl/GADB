using AutoMapper;
using GADB.Server.Models.DB;
using GADB.Shared.Models;

namespace GADB.Server.Models.Mapper
{
    public class DataMapping : Profile

    {
        public DataMapping()
        {
            CreateMap<Tdata, Data>().ForMember(d => d.DataElements, opt => opt.Ignore()).ReverseMap();
        }
    }
}