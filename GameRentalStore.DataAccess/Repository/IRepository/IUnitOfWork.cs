namespace GameRentalStore.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IGenreRepository Genre { get; }
        IGameRepository Game { get; }
        IGameMediaRepository GameMedia { get; }
        IShoppingCartRepository ShoppingCart { get; }
        ISubscriptionPackageRepository SubscriptionPackage { get; }
        IUserPackageRepository UserPackage { get; }
        IApplicationUserRepository ApplicationUser { get; }


        void Save();
        Task SaveAsync();
    }
}
