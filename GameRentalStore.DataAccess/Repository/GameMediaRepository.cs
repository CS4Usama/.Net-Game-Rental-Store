using GameRentalStore.DataAccess.Data;
using GameRentalStore.DataAccess.Repository.IRepository;
using GameRentalStore.Models;

namespace GameRentalStore.DataAccess.Repository
{
    public class GameMediaRepository : Repository<GameMedia>, IGameMediaRepository
    {
        private readonly ApplicationDbContext _db;
        public GameMediaRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(GameMedia obj)
        {
            _db.GameMedias.Update(obj);
        }
    }
}
