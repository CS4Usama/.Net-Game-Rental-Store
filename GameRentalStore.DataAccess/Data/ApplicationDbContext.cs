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
        public DbSet<GameRating> GameRatings { get; set; }
        public DbSet<GameMedia> GameMedias { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<SubscriptionPackage> SubscriptionPackages { get; set; }
        public DbSet<UserPackage> UserPackages { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<SubscriptionPackage>().HasData(
               new SubscriptionPackage { Id = 1, PackageName = "Basic", GamesPerMonth = 2, RentNewReleasedGame = 0, MaxReplacePerMonth = 0 },
               new SubscriptionPackage { Id = 2, PackageName = "Premium Max", GamesPerMonth = 3, RentNewReleasedGame = 2, MaxReplacePerMonth = 3 },
               new SubscriptionPackage { Id = 3, PackageName = "Premium", GamesPerMonth = 2, RentNewReleasedGame = 1, MaxReplacePerMonth = 2 }
            );


            modelBuilder.Entity<Genre>().HasData(
               new Genre { Id = 1, Name = "Action", DisplayOrder = 1 },
               new Genre { Id = 2, Name = "Adventure", DisplayOrder = 2 },
               new Genre { Id = 3, Name = "Role Playing", DisplayOrder = 3 },
               new Genre { Id = 4, Name = "Shooter", DisplayOrder = 4 },
               new Genre { Id = 5, Name = "Simulator", DisplayOrder = 5 },
               new Genre { Id = 6, Name = "Puzzle", DisplayOrder = 6 }
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
                    GenreId = 3,
                },
                new Game
                {
                    Id = 2,
                    Title = "Children of the Sun",
                    Description = "Burning with anger, The girl wages a one-woman war against the cult, taking them down cultist by cultist, bullet by bullet, until she reaches her true target: the leader.",
                    Platform = "Nintendo Switch",
                    ReleaseDate = new DateOnly(2024, 04, 09),
                    Quantity = 10,
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
                    GenreId = 3
                }
            );


            modelBuilder.Entity<GameMedia>().HasData(
                new GameMedia { Id = 1, MediaUrl = "\\media\\games\\game-1\\4a77a065-a27f-4c79-9d45-90a0e6c7015b.webp", MediaType = "image", GameId = 1 },
                new GameMedia { Id = 2, MediaUrl = "https://www.youtube.com/embed/3svNksUg_BM", MediaType = "video", GameId = 1 },
                new GameMedia { Id = 3, MediaUrl = "\\media\\games\\game-1\\ea3d5b3c-c20a-4d4b-affc-0c4d0da61430.webp", MediaType = "image", GameId = 1 },
                new GameMedia { Id = 4, MediaUrl = "\\media\\games\\game-1\\0e07a077-5f67-4d7e-8c1e-863e5dcead70.webp", MediaType = "image", GameId = 1 },
                new GameMedia { Id = 5, MediaUrl = "\\media\\games\\game-1\\c6402a57-debe-48f6-8428-d60ad4997254.webp", MediaType = "image", GameId = 1 },
                new GameMedia { Id = 6, MediaUrl = "\\media\\games\\game-2\\e7378d53-6faa-47f8-8c67-b4ecfd9c41f9.webp", MediaType = "image", GameId = 2 },
                new GameMedia { Id = 7, MediaUrl = "https://www.youtube.com/embed/Jp4miWjVHtY", MediaType = "video", GameId = 2 },
                new GameMedia { Id = 8, MediaUrl = "\\media\\games\\game-2\\2ead7900-d473-47af-b367-93bb8a09c5f6.webp", MediaType = "image", GameId = 2 },
                new GameMedia { Id = 9, MediaUrl = "\\media\\games\\game-2\\94066954-7138-4a2e-924d-f149e300fb7a.webp", MediaType = "image", GameId = 2 },
                new GameMedia { Id = 10, MediaUrl = "\\media\\games\\game-2\\1ffb394b-aa03-44b4-a79a-bee9b771b05b.webp", MediaType = "image", GameId = 2 },
                new GameMedia { Id = 11, MediaUrl = "\\media\\games\\game-3\\8a2bb983-fda4-4015-9697-3442d9c989ee.webp", MediaType = "image", GameId = 3 },
                new GameMedia { Id = 12, MediaUrl = "https://www.youtube.com/embed/Qp0v7IVw4aA", MediaType = "video", GameId = 3 },
                new GameMedia { Id = 13, MediaUrl = "\\media\\games\\game-3\\77a06a13-f83a-40a0-af26-31ae3f8ef86e.webp", MediaType = "image", GameId = 3 },
                new GameMedia { Id = 14, MediaUrl = "\\media\\games\\game-3\\990afc77-95cd-41b5-9c28-4da1bf8a6688.webp", MediaType = "image", GameId = 3 },
                new GameMedia { Id = 15, MediaUrl = "\\media\\games\\game-3\\f5ad9b4e-08a0-453e-89eb-500aaf156685.webp", MediaType = "image", GameId = 3 },
                new GameMedia { Id = 16, MediaUrl = "\\media\\games\\game-4\\9972760f-bb59-4319-b145-357963e8f4a3.webp", MediaType = "image", GameId = 4 },
                new GameMedia { Id = 17, MediaUrl = "https://www.youtube.com/embed/vJcp6CMkUuA", MediaType = "video", GameId = 4 },
                new GameMedia { Id = 18, MediaUrl = "\\media\\games\\game-4\\9f30b7b1-aacb-42a9-a5f6-8473d7809567.webp", MediaType = "image", GameId = 4 },
                new GameMedia { Id = 19, MediaUrl = "\\media\\games\\game-4\\209c58e2-ff8d-4875-884e-f1e46e5e05af.webp", MediaType = "image", GameId = 4 },
                new GameMedia { Id = 20, MediaUrl = "\\media\\games\\game-4\\8d54d69b-2e58-4ea2-b71f-c6c9a10fc54b.webp", MediaType = "image", GameId = 4 },
                new GameMedia { Id = 21, MediaUrl = "\\media\\games\\game-5\\0cd268dc-36f5-488d-95e2-b2ce6c2791aa.webp", MediaType = "image", GameId = 5 },
                new GameMedia { Id = 22, MediaUrl = "https://www.youtube.com/embed/xC9FXqO5C2s", MediaType = "video", GameId = 5 },
                new GameMedia { Id = 23, MediaUrl = "\\media\\games\\game-5\\3a1fba0d-5b69-493f-8f3e-fc7572515f65.webp", MediaType = "image", GameId = 5 },
                new GameMedia { Id = 24, MediaUrl = "\\media\\games\\game-5\\ca3d1eb9-8750-495f-8337-ac4a92eda825.webp", MediaType = "image", GameId = 5 },
                new GameMedia { Id = 25, MediaUrl = "\\media\\games\\game-5\\c19de547-8615-4abd-a729-dca5d8fb601e.webp", MediaType = "image", GameId = 5 }
            );
        }
    }
}
