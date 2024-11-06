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
    public class PaymentService : Service<Payment>, IPaymentService
    {
        public PaymentService(IGenericRepository<Payment> repository, IUnitOfWorks unitOfWorks) : base(repository, unitOfWorks)
        {
        }

    }
}
