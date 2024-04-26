using System.Linq.Expressions;

namespace App.Data.Repositories.Base;

public interface IGenericRepositoryAsync<T> where T : class
{
    IQueryable<T> GetList(Expression<Func<T, bool>> filter = null);

    Task<T> GetItemAsync(int id);

    Task<bool> CreateAsync(T item);
    Task<bool> EditAsync(T item);

    Task<bool> DeleteAsync(object id);
    Task<bool> DeleteAsync(T item);
}
