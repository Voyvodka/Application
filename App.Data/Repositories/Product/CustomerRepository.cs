using App.Data.Model.Entities.Product;

namespace App.Data.Repositories.Product;

public class CustomerRepository : GenericRepositoryAsync<Customer>
{
    public CustomerRepository(DbContext context, IMapper mapper) : base(context, mapper)
    {
    }
}
