using EvrenCo.Core.Models;
using EvrenCo.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvrenCo.Repository.Repositories
{
    public class DepartmentRepository(AppDbContext context):GenericRepository<Department>(context),IDepartmentRepository
    {
    }
}
