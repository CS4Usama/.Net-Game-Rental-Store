using System.Linq.Expressions;

namespace GameRentalStore.DataAccess.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        // T - Category
        IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, string? includeProperties = null, bool tracked = false);    //Get All Category
        T Get(Expression<Func<T, bool>> filter, string? includeProperties = null, bool tracked = false);    //Get Single Category
        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);
        void Add(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entity);
    }
}
