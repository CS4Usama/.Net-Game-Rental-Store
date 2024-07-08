using GameRentalStore.DataAccess.Data;
using GameRentalStore.DataAccess.Repository.IRepository;
using GameRentalStore.Models;

namespace GameRentalStore.DataAccess.Repository
{
    public class UserPackageRepository : Repository<UserPackage>, IUserPackageRepository
    {
        private readonly ApplicationDbContext _db;
        public UserPackageRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(UserPackage obj)
        {
            _db.UserPackages.Update(obj);
        }
    }
}
