using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XYApp.Models;

namespace XYApp.Data
{
    public class POIContexto : DbContext
    {
       
        private readonly string strConexao = "Server=SERVIDOR;Database=DB_XYAPPPOI;Trusted_Connection=True;MultipleActiveResultSets=true"; 
        public POIContexto()
        { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(strConexao);
        }
        public DbSet<POI> POIs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<POI>()
               .Property(u => u.NomePOI)
               .IsUnicode(false)
               .HasMaxLength(50)
               .IsRequired(); // NOT NULL

            modelBuilder.Entity<POI>().ToTable("POI");         
        }
    }
}
