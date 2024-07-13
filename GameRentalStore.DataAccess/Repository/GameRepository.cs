using GameRentalStore.DataAccess.Data;
using GameRentalStore.DataAccess.Repository.IRepository;
using GameRentalStore.Models;

namespace GameRentalStore.DataAccess.Repository
{
    public class GameRepository : Repository<Game>, IGameRepository
    {
        private readonly ApplicationDbContext _db;
        public GameRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Game obj)
        {
            //_db.Products.Update(obj);

            var objFromDb = _db.Games.FirstOrDefault(u => u.Id == obj.Id);
            if (objFromDb != null)
            {
                objFromDb.Title = obj.Title;
                objFromDb.Description = obj.Description;
                objFromDb.Platform = obj.Platform;
                objFromDb.ReleaseDate = obj.ReleaseDate;
                objFromDb.Quantity = obj.Quantity;
                objFromDb.GenreId = obj.GenreId;
                objFromDb.GameMedias = obj.GameMedias;
            }
        }
    }
}
