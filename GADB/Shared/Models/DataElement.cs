using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GADB.Shared.Models
{
   public class DataElement
    {
        public string Name { get; set; }
        public string Datatype { get; set; }
        public Guid? ReferenceId { get; set; }
        public string Value { get; set; }
        public int Sort { get; set; }
    }
}
