using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GADB.Shared.Models
{
    public class Data
    {
        public Guid Id { get; set; }
        public Guid DocId { get; set; }
        public string Elements { get; set; }
        public List<DataElement>DataElements { get; set; }
        public Documenttype Doc { get; set; }
    }
}
