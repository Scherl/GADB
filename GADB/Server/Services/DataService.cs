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

        public async Task<IList<Data>> GetAll()
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
            return list;
        }

        public async Task<Data> GetById(Guid id)
        {
            var tItem = await _context.Tdata.FindAsync(id);
            var item = _mapper.Map<Data>(tItem);
            item.DataElements = new List<DataElement>(JsonSerializer.Deserialize<IList<DataElement>>(item.Elements));
            return item;
        }

        public async Task<Data> GetHQ(Guid id)
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

            return itemData;
        }

        public async Task<IList<Data>> GetSubsidiaries(Guid id)
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
            return list;
        }





    }
}
