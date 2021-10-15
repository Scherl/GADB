using AutoMapper;
using GADB.Server.Interfaces;
using GADB.Shared.Models;
using System.Text.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GADB.Server.Models.DB;

namespace GADB.Server.Services
{
    public class DataService: IDataService
    {
        private readonly GADBContext  _context;
        private readonly IMapper _mapper;

        public DataService(GADBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }

        // get HQ by id
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
    }
}
