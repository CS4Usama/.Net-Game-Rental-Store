using GameRentalStore.Models;

namespace GameRentalStore.DataAccess.Repository.IRepository
{
    public interface ISubscriptionPackageRepository : IRepository<SubscriptionPackage>
    {
        void Update(SubscriptionPackage obj);
    }
}
