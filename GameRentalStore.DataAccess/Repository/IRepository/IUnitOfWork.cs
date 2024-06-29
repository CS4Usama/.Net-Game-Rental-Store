namespace GameRentalStore.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IGenreRepository Genre { get; }

        void Save();
    }
}
