using GameRentalStore.DataAccess.Data;
using GameRentalStore.DataAccess.Repository.IRepository;

namespace GameRentalStore.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _db;
        public IGenreRepository Genre { get; private set; }
        public IGameRepository Game { get; private set; }
        public IGameMediaRepository GameMedia { get; private set; }
        public IShoppingCartRepository ShoppingCart { get; private set; }


        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Genre = new GenreRepository(_db);
            Game = new GameRepository(_db);
            GameMedia = new GameMediaRepository(_db);
            ShoppingCart = new ShoppingCartRepository(_db);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
