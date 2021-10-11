using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace GADB.Server.Models.DB
{
    [Table("TReferenceValue")]
    public partial class TreferenceValue
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [StringLength(1000)]
        public string Name { get; set; }
        [Required]
        public string Value { get; set; }
    }
}
