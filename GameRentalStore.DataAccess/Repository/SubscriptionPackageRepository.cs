using GameRentalStore.DataAccess.Data;
using GameRentalStore.DataAccess.Repository.IRepository;
using GameRentalStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GameRentalStore.DataAccess.Repository
{
    public class SubscriptionPackageRepository : Repository<SubscriptionPackage>, ISubscriptionPackageRepository
    {
        private readonly ApplicationDbContext _db;
        public SubscriptionPackageRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(SubscriptionPackage obj)
        {
            _db.SubscriptionPackages.Update(obj);
        }
    }
}
