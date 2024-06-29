using GameRentalStore.DataAccess.Data;
using GameRentalStore.DataAccess.Repository.IRepository;
using GameRentalStore.Models;
using Microsoft.EntityFrameworkCore;

namespace GameRentalStore.DataAccess.Repository
{
    public class GenreRepository : Repository<Genre>, IGenreRepository
    {
        private readonly ApplicationDbContext _db;
        public GenreRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Genre obj)
        {
            _db.Genres.AsNoTracking();
            _db.Genres.Update(obj);
        }
    }
}
