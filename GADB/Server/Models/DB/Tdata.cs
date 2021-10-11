using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace GADB.Server.Models.DB
{
    [Table("TData")]
    public partial class Tdata
    {
        [Key]
        public Guid Id { get; set; }
        public Guid DocId { get; set; }
        [Required]
        public string Elements { get; set; }

        [ForeignKey(nameof(DocId))]
        [InverseProperty(nameof(Tdocumenttype.Tdata))]
        public virtual Tdocumenttype Doc { get; set; }
    }
}
