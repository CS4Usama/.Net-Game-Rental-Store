using GameRentalStore.Models;

namespace GameRentalStore.DataAccess.Repository.IRepository
{
    public interface IGameRatingRepository : IRepository<GameRating>
    {
        void Update(GameRating obj);
    }
}
