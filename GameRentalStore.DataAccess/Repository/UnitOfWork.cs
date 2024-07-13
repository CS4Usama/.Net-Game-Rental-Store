using GameRentalStore.DataAccess.Data;
using GameRentalStore.DataAccess.Repository.IRepository;

namespace GameRentalStore.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _db;
        public IGenreRepository Genre { get; private set; }
        public IGameRepository Game { get; private set; }
        public IGameRatingRepository GameRating { get; private set; }
        public IGameMediaRepository GameMedia { get; private set; }
        public IShoppingCartRepository ShoppingCart { get; private set; }
        public ISubscriptionPackageRepository SubscriptionPackage { get; private set; }
        public IUserPackageRepository UserPackage { get; private set; }
        public IApplicationUserRepository ApplicationUser { get; private set; }


        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Genre = new GenreRepository(_db);
            Game = new GameRepository(_db);
            GameRating = new GameRatingRepository(_db);
            GameMedia = new GameMediaRepository(_db);
            ShoppingCart = new ShoppingCartRepository(_db);
            SubscriptionPackage = new SubscriptionPackageRepository(_db);
            UserPackage = new UserPackageRepository(_db);
            ApplicationUser = new ApplicationUserRepository(_db);
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}
