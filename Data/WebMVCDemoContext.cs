using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebMVCDemo.Models;

namespace WebMVCDemo.Data
{
    public class WebMVCDemoContext : DbContext
    {
        public WebMVCDemoContext (DbContextOptions<WebMVCDemoContext> options)
            : base(options)
        {
        }

        public DbSet<WebMVCDemo.Models.Kontingent> Kontingent { get; set; } = default!;

        public DbSet<WebMVCDemo.Models.Medlem> Medlem { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "English_United States.1252)");
            modelBuilder.Entity<Medlem>(entity =>
            {
                modelBuilder.Entity<Medlem>()
                    .HasOne<Kontingent>(s => s.Kontingent)
                    .WithMany(g => g.Medlems)
                    .HasForeignKey(s => s.CurrentKontintId);
            });
            modelBuilder.Entity<Kontingent>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(50);
            });
        }


    }

    


}
