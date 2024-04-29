using App.Data.Model.Entities.Product;
using App.Data.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace App.Data.Repositories.Product;
public class StockRepository : GenericRepositoryAsync<Stock>
{
    public StockRepository(DbContext context) : base(context)
    {
    }

    public async Task<Stock> GetItemForShow(int id)
    {
        return await Conn.Stocks.Include(c => c.Unit).FirstOrDefaultAsync(c => c.Id == id);
    }
}