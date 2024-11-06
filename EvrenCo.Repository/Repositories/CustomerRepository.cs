using EvrenCo.Core.Models;
using EvrenCo.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvrenCo.Repository.Repositories
{
    public class CustomerRepository(AppDbContext context):GenericRepository<Customer>(context),ICustomerRepository
    {
    }
}
