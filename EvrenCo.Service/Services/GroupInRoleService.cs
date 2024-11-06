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
    public class GroupInRoleService : Service<GroupInRole>, IGroupInRoleService
    {
        public GroupInRoleService(IGenericRepository<GroupInRole> repository, IUnitOfWorks unitOfWorks) : base(repository, unitOfWorks)
        {
        }
    }
}
