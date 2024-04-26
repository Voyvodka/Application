using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace App.Data.Repositories.Base;

public class GenericRepositoryAsync<T> : IGenericRepositoryAsync<T> where T : class
{
    protected DbContext Context;
    protected DbSet<T> DbSet;

    public GenericRepositoryAsync(DbContext context)
    {
        Context = context;
        if (context != null)
        {
            DbSet = context.Set<T>();
        }
    }

    public virtual IQueryable<T> GetList(Expression<Func<T, bool>> filter = null)
    {
        IQueryable<T> query = DbSet.AsNoTracking();
        if (filter != null)
            query = query.Where(filter);
        return query;
    }
    public virtual async Task<T> GetItemAsync(int id)
    {
        return await DbSet.FindAsync(id);
    }

    public virtual async Task<bool> CreateAsync(T item)
    {
        DbSet.Add(item);
        await Context.SaveChangesAsync();
        return true;
    }

    public virtual async Task<bool> EditAsync(T item)
    {
        Context.Entry(item).State = EntityState.Modified;
        await Context.SaveChangesAsync();
        return true;
    }

    public virtual async Task<bool> DeleteAsync(object id)
    {
        T entityToDelete = await DbSet.FindAsync(id);
        return await DeleteAsync(entityToDelete);
    }
    public virtual async Task<bool> DeleteAsync(T item)
    {
        if (Context.Entry(item).State == EntityState.Detached)
            DbSet.Attach(item);
        DbSet.Remove(item);
        await Context.SaveChangesAsync();
        return true;
    }
}