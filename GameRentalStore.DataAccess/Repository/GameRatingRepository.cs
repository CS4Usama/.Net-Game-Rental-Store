using GameRentalStore.DataAccess.Data;
using GameRentalStore.DataAccess.Repository.IRepository;
using GameRentalStore.Models;

namespace GameRentalStore.DataAccess.Repository
{
    public class GameRatingRepository : Repository<GameRating>, IGameRatingRepository
    {
        private readonly ApplicationDbContext _db;
        public GameRatingRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(GameRating obj)
        {
            _db.GameRatings.Update(obj);
        }
    }
}
