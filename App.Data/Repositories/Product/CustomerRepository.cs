using App.Data.Model.Entities.Product;
using App.Data.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace App.Data.Repositories.Product;

public class CustomerRepository : GenericRepositoryAsync<Customer>
{
    public CustomerRepository(DbContext context) : base(context)
    {
    }
}
