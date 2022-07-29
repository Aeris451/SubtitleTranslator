using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SubtitleTranslator.Model;
using System.Configuration;

namespace SubtitleTranslator.DAL
{
    public class ApplicationDbContext : DbContext
    {
        //table properties
        public DbSet<Translation> Translations { get; set; }
        public DbSet<SrtLine> SrtLines { get; set; }


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }
        // metoda definiująca elementy konfiguracyjne
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            
        }
        // metoda przechowująca elementy definicji modelu
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            builder.Entity<SrtLine>()
                  .HasOne(s => s.Translation)
                  .WithMany(stg => stg.srtLines)
                  .HasForeignKey(x => x.TranslationId)
                  .OnDelete(DeleteBehavior.Cascade);






            builder.Entity<Translation>()
                  .Property(f => f.TranslationId)
                  .ValueGeneratedOnAdd();


        }
    }

}
