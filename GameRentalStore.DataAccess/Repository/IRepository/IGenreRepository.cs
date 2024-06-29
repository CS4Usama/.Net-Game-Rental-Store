using GameRentalStore.Models;

namespace GameRentalStore.DataAccess.Repository.IRepository
{
    public interface IGenreRepository : IRepository<Genre>
    {
        void Update(Genre obj);
    }
}
