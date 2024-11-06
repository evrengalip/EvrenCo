using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvrenCo.Core.Models
{
    public class Customer:BaseEntity
    {
        public string Name { get; set; }
        public ICollection<Payment> Payments { get; set; }

        public ICollection<Sale> Sales { get; set; }
    }
}
