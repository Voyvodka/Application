using App.Services.Models;
using AutoMapper;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;

namespace App.Services.Extenders;

public static class QueryExtenders
{
    /// <summary>
    /// Verilen sorguyu filtre ve sıralama kriterlerine göre dinamik olarak düzenler.
    /// Eğer filtre ve sıralama bilgisi sağlanmazsa, varsayılan alan adı ve yönü kullanılır.
    /// </summary>
    /// <typeparam name="T">Sorgulanan varlık türü.</typeparam>
    /// <param name="query">Filtre ve sıralama uygulanacak sorgu.</param>
    /// <param name="p">Filtre ve sıralama bilgilerini içeren parametreler.</param>
    /// <param name="defaultField">Varsayılan sıralama alanı (varsayılan: "Name").</param>
    /// <param name="defaultDir">Varsayılan sıralama yönü (varsayılan: "ASC").</param>
    /// <returns>Filtrelenmiş ve sıralanmış sorgu.</returns>
    public static IQueryable<T> ToDynamicWhereAndOrder<T>(this IQueryable<T> query, ApiListPostModel p,
        string defaultField = "Name", string defaultDir = "ASC")
        where T : class
    {
        if (!string.IsNullOrWhiteSpace(p.Filter))
            query = query.Where(p.Filter);

        if (!string.IsNullOrWhiteSpace(p.OrderDir) && !string.IsNullOrWhiteSpace(p.OrderField))
            query = query.OrderBy($"{p.OrderField} {p.OrderDir}");
        else
            query = query.OrderBy($"{defaultField} {defaultDir}");

        return query;
    }

    /// <summary>
    /// Verilen sorguyu, belirtilen DTO türüne dönüştürür.
    /// AutoMapper kullanılarak sorgudaki varlıklar DTO'ya projekte edilir.
    /// </summary>
    /// <typeparam name="T">Veri tabanı varlık türü.</typeparam>
    /// <typeparam name="Z">Projeksiyon yapılacak DTO türü.</typeparam>
    /// <param name="query">Projeksiyon yapılacak sorgu.</param>
    /// <param name="mapper">AutoMapper örneği.</param>
    /// <returns>DTO'ya projekte edilmiş sorgu.</returns>
    public static IQueryable<Z> QueryResultDto<T, Z>(this IQueryable<T> query, IMapper mapper)
        where T : class
        where Z : class
    {
        return mapper.ProjectTo<Z>(query);
    }

    /// <summary>
    /// Verilen sorguyu belirli bir sayfalama parametrelerine göre sayfalar.
    /// </summary>
    /// <typeparam name="T">Sorgulanan varlık türü.</typeparam>
    /// <param name="query">Sayfalama uygulanacak sorgu.</param>
    /// <param name="p">Sayfalama bilgilerini içeren parametreler (sayfa boyutu, sayfa endeksi vb.).</param>
    /// <returns>Sayfalanmış verileri ve sayfa bilgilerini içeren model.</returns>
    public static async Task<ApiResultPagerModel<T>> ToPagedResultAsync<T>(this IQueryable<T> query, ApiListPostModel p)
    where T : class
    {
        var model = new ApiResultPagerModel<T>
        {
            Result = 1,
            CurrentPageIndex = p.PageIndex,
            CurrentPageSize = p.PageSize,
            TotalItemsCount = await query.CountAsync()
        };

        if (model.TotalItemsCount > 0)
            model.TotalPageCount = (int)Math.Ceiling(model.TotalItemsCount / (double)model.CurrentPageSize);

        if (model.CurrentPageIndex > model.TotalPageCount)
            model.CurrentPageIndex = model.TotalPageCount;

        int start = (model.CurrentPageIndex - 1) * model.CurrentPageSize;

        if (model.TotalPageCount > 0)
            model.Items = await query.Skip(start)
                                    .Take(model.CurrentPageSize)
                                    .ToListAsync();

        return model;
    }

}
