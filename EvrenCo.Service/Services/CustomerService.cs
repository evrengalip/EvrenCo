using EvrenCo.Core.Models;
using EvrenCo.Core.Repositories;
using EvrenCo.Core.Services;
using EvrenCo.Core.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvrenCo.Service.Services
{
    public class CustomerService(IGenericRepository<Customer> repository, IUnitOfWorks unitOfWorks,ICustomerRepository customerRepository) : Service<Customer>(repository, unitOfWorks), ICustomerService
    {
        private readonly ICustomerRepository _customerRepository = customerRepository;
    }
}
