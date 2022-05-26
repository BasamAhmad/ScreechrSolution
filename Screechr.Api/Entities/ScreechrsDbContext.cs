using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Screechr.Api.Models;

namespace Screechr.Api.Entities
{
    /// <summary>
    /// The screechrs db context.
    /// </summary>
    public partial class ScreechrsDbContext : DbContext
    {
        /// <summary>
        /// Gets or sets the users.
        /// </summary>
        public DbSet<User> Users { get; set; }
        /// <summary>
        /// Gets or sets the screechs.
        /// </summary>
        public DbSet<Screech> Screechs { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="ScreechrsDbContext"/> class.
        /// </summary>
        public ScreechrsDbContext()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ScreechrsDbContext"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        public ScreechrsDbContext(DbContextOptions<ScreechrsDbContext> options) : base(options)
        {
        }

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        public virtual DbSet<User> User { get; set; }
        /// <summary>
        /// Gets or sets the screech.
        /// </summary>
        public virtual DbSet<Screech> Screech { get; set; }
        /// <summary>
        /// Ons the model creating.
        /// </summary>
        /// <param name="modelBuilder">The model builder.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Id is key
            modelBuilder.Entity<User>()
            .HasIndex(b => b.Id)
            .IsUnique();

            //Id is key
            modelBuilder.Entity<Screech>()
            .HasIndex(b => b.Id)
            .IsUnique();
        }
    }
}
