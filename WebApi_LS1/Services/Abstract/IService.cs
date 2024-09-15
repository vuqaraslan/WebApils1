using System.Linq.Expressions;

namespace WebApi_LS1.Services.Abstract
{
    public interface IService<T> 
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> Get(Expression<Func<T, bool>> predicate);
        Task Add(T entity);
        Task Update(T entity);
        Task Delete(T entity);
    }
}
