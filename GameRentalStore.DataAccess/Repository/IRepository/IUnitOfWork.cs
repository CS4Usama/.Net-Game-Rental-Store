namespace GameRentalStore.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IGenreRepository Genre { get; }
        IGameRepository Game { get; }
        IGameMediaRepository GameMedia { get; }
        IShoppingCartRepository ShoppingCart { get; }

        void Save();
    }
}
