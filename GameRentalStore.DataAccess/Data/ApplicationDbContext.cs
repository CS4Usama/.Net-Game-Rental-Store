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
        public DbSet<Game> Games { get; set; }
        public DbSet<GameMedia> GameMedias { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<SubscriptionPackage> SubscriptionPackages { get; set; }
        public DbSet<UserPackage> UserPackages { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Genre>().HasData(
               new Genre { Id = 1, Name = "Action", DisplayOrder = 1 },
               new Genre { Id = 2, Name = "Adventure", DisplayOrder = 2 },
               new Genre { Id = 3, Name = "Role Playing", DisplayOrder = 3 }
            );

            modelBuilder.Entity<SubscriptionPackage>().HasData(
               new SubscriptionPackage { Id = 1, PackageName = "Basic", GamesPerMonth = 2, RentNewReleasedGame = 0, MaxReplacePerMonth = 0 },
               new SubscriptionPackage { Id = 2, PackageName = "Premium Max", GamesPerMonth = 3, RentNewReleasedGame = 2, MaxReplacePerMonth = 3 },
               new SubscriptionPackage { Id = 3, PackageName = "Premium", GamesPerMonth = 2, RentNewReleasedGame = 1, MaxReplacePerMonth = 2 }
            );


            modelBuilder.Entity<Game>().HasData(
                new Game
                {
                    Id = 1,
                    Title = "Still Wakes the Deep",
                    Description = "1975. Disaster strikes the Beira D oil rig off the coast of Scotland. Navigate the collapsing rig to save your crew from an otherworldly horror on the edge of all logic and reality.",
                    Platform = "PC",
                    ReleaseDate = new DateOnly(2024, 06, 18),
                    Quantity = 7,
                    Rating = 4,
                    GenreId = 3
                },
                new Game
                {
                    Id = 2,
                    Title = "Children of the Sun",
                    Description = "Burning with anger, The girl wages a one-woman war against the cult, taking them down cultist by cultist, bullet by bullet, until she reaches her true target: the leader.",
                    Platform = "Nintendo Switch",
                    ReleaseDate = new DateOnly(2024, 04, 09),
                    Quantity = 10,
                    Rating = 3,
                    GenreId = 4
                },
                new Game
                {
                    Id = 3,
                    Title = "Galacticare",
                    Description = "Save the galaxy - one patient at a time! As the new director of Galacticare, you will build and manage a series of hospitals to keep your patients alive for as long as possible - for money!",
                    Platform = "PC",
                    ReleaseDate = new DateOnly(2024, 05, 23),
                    Quantity = 8,
                    Rating = 5,
                    GenreId = 5
                },
                new Game
                {
                    Id = 4,
                    Title = "Botany Manor",
                    Description = "Welcome to Botany Manor, a stately home in 19th century England. You play as its inhabitant Arabella Greene, a retired botanist.",
                    Platform = "Xbox",
                    ReleaseDate = new DateOnly(2024, 02, 09),
                    Quantity = 6,
                    Rating = 2,
                    GenreId = 6
                },
                new Game
                {
                    Id = 5,
                    Title = "Dead Island 2: SoLA",
                    Description = "Stage dive into the heart of SoLA, the ultimate Californian music festival, built upon ancient grounds...",
                    Platform = "Playstation",
                    ReleaseDate = new DateOnly(2024, 03, 17),
                    Quantity = 9,
                    Rating = 3,
                    GenreId = 3
                }
            );
        }
    }
}
