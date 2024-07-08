using GameRentalStore.Models;

namespace GameRentalStore.DataAccess.Repository.IRepository
{
    public interface IUserPackageRepository : IRepository<UserPackage>
    {
        void Update(UserPackage obj);
    }
}
