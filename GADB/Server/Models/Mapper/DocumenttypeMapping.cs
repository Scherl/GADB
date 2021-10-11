using AutoMapper;
using GADB.Server.Models.DB;
using GADB.Shared.Models;

namespace GADB.Server.Models.Mapper
{
    public class DocumenttypeMapping : Profile
    {
        public DocumenttypeMapping()
        {
            CreateMap<Tdocumenttype, Documenttype>().ForMember(d => d.DataElements, opt => opt.Ignore()).ReverseMap();
        }
    }
}
