using App.Data.Model.Entities.Product;

namespace App.Data.Repositories.Product;
public class StockRepository : GenericRepositoryAsync<Stock>
{
    public StockRepository(DbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<Stock> GetItemForShow(int id)
    {
        return await Conn.Stocks.Include(c => c.Unit).FirstOrDefaultAsync(c => c.Id == id);
    }
}