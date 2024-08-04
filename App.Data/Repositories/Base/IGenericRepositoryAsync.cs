using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace App.Data.Repositories.Base;

public interface IGenericRepositoryAsync<T> where T : class
{
    IQueryable<T> GetList(Expression<Func<T, bool>> filter = null);

    Task<T> GetItemAsync(int id);

    Task<bool> CreateAsync(T item);
    Task<bool> EditAsync(T item);

    Task<bool> DeleteAsync(object id);
    Task<bool> DeleteAsync(T item);
    Task<List<SelectListItem>> GetSelectListItems(object id, Expression<Func<T, bool>> filter = null);
}
