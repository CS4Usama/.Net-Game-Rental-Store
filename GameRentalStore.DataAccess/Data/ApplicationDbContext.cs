using System.Collections.Generic;
using System.Reflection.Emit;
using GameRentalStore.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GameRentalStore.DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Genre> Genres { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Genre>().HasData(
               new Genre { Id = 1, Name = "Action", DisplayOrder = 1 },
               new Genre { Id = 2, Name = "Adventure", DisplayOrder = 2 },
               new Genre { Id = 3, Name = "Role Playing", DisplayOrder = 3 }
            );
        }
    }
}
