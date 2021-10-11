using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace GADB.Server.Models.DB
{
    public partial class GADBContext : DbContext
    {
        public GADBContext()
        {
        }

        public GADBContext(DbContextOptions<GADBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Tdata> Tdata { get; set; }
        public virtual DbSet<Tdatatype> Tdatatype { get; set; }
        public virtual DbSet<Tdocumenttype> Tdocumenttype { get; set; }
        public virtual DbSet<TreferenceValue> TreferenceValue { get; set; }

     
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Latin1_General_CI_AS");

            modelBuilder.Entity<Tdata>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.Doc)
                    .WithMany(p => p.Tdata)
                    .HasForeignKey(d => d.DocId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Data_Documenttype");
            });

            modelBuilder.Entity<Tdatatype>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<Tdocumenttype>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<TreferenceValue>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
