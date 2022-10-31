using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebEFMVCDemo.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Numerics;

namespace WebEFMVCDemo.Data
{
    public class WebEFMVCDemoContext : DbContext
    {
        public WebEFMVCDemoContext (DbContextOptions<WebEFMVCDemoContext> options)
            : base(options)
        {
        }

        public DbSet<WebEFMVCDemo.Models.Kontingent> Kontingents { get; set; } 
        public DbSet<WebEFMVCDemo.Models.Medlem> Medlems { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Medlem>(entity =>
            {
                modelBuilder.Entity<Medlem>()
                    .HasOne<Kontingent>(s => s.Kontingent)
                    .WithMany(g => g.Medlems)
                    .HasForeignKey(s => s.KontintId);
            });
            
            modelBuilder.Entity<Kontingent>().HasData(
                new Kontingent() { KontintId = 1, Name = "betalt"},
                new Kontingent() { KontintId = 2, Name = "ikke betalt" } 
            );
        }
    }
}
