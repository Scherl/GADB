using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace GADB.Server.Models.DB
{
    [Table("TDocumenttype")]
    public partial class Tdocumenttype
    {
        public Tdocumenttype()
        {
            Tdata = new HashSet<Tdata>();
        }

        [Key]
        public Guid Id { get; set; }
        [Required]
        [StringLength(1000)]
        public string Name { get; set; }
        public string Elements { get; set; }
        [Column(TypeName = "decimal(5, 1)")]
        public decimal Version { get; set; }

        [InverseProperty("Doc")]
        public virtual ICollection<Tdata> Tdata { get; set; }
    }
}
