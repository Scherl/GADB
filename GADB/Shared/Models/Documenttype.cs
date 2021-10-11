using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GADB.Shared.Models
{
    public class Documenttype
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Elements { get; set; }
        [Column(TypeName = "decimal(5, 1)")]
        public decimal Version { get; set; }
        public List<DataElement>DataElements { get; set; }
    }
}
