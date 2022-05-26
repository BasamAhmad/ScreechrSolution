using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Screechr.Api.Models;

namespace Screechr.Api.Entities
{
    public partial class ScreechrsDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public ScreechrsDbContext()
        {
        }

        public ScreechrsDbContext(DbContextOptions<ScreechrsDbContext> options) : base(options)
        {
        }

        public virtual DbSet<User> User { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        { 
            //Id is key
            modelBuilder.Entity<User>()
            .HasIndex(b => b.Id);
        }
    }
}
