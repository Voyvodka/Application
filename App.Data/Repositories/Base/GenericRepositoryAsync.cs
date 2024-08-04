using System.Linq.Expressions;
using App.Data.Model.Entities.Base;
using App.Services.Extenders;
using App.Services.Reflection;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace App.Data.Repositories.Base;

public class GenericRepositoryAsync<T> : IGenericRepositoryAsync<T> where T : class
{
    protected DbContext Context;
    protected DbSet<T> DbSet;

    public AppData Conn => Context as AppData;

    private string _currentUserId;
    public string CurrentUserId
    {
        get
        {
            return _currentUserId;
        }
        set
        {
            _currentUserId = value;
            Conn.CurrentUserId = _currentUserId;
        }
    }


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
        if (typeof(T).IsSubclassOf(typeof(BaseDeleteEntity)) || typeof(T).IsSubclassOf(typeof(BaseCreateEntity)))
        {
            if (CurrentUserId.IsEmpty())
                throw new Exception("CREATE Geçersiz kullanıcı bilgisi");

            Digger.SetObjectValue(item, "CreatorId", CurrentUserId);
            Digger.SetObjectValue(item, "CreatedOn", DateTime.Now);
        }
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

    public virtual async Task<List<SelectListItem>> GetSelectListItems(object id = null, Expression<Func<T, bool>> filter = null)
    {
        if (!typeof(T).IsSubclassOf(typeof(BaseEntity)))
        {
            return [];
        }
        IQueryable<T> query = DbSet.AsNoTracking();
        if (filter != null)
            query = query.Where(filter);
        return await query.Select(c => new SelectListItem()
        {
            Value = EF.Property<int>(c, "Id").ToString(),
            Text = EF.Property<string>(c, "Name"),
            Selected = id != null && EF.Property<int>(c, "Id") == (int)id
        })
        .AsNoTracking()
        .AsSplitQuery()
        .ToListAsync();
    }
}