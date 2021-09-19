using CommandService.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommandService.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {
        }

        public DbSet<Platform> Platforms { get; set; }
        public DbSet<Command> Commands { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Platform>()
                         .HasMany<Command>(c => c.Commands)
                         .WithOne(p => p.Platform)
                         .HasForeignKey(c => c.PlatformId);

            modelBuilder.Entity<Command>()
                         .HasOne(p => p.Platform)
                         .WithMany(c => c.Commands)
                         .HasForeignKey(c => c.PlatformId);

            //base.OnModelCreating(modelBuilder);
        }
    }
}
