using EvrenCo.Core.DTOs;
using EvrenCo.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvrenCo.Core.Services
{
    public interface IUserService:IService<User>
    {
        User GetByEmail(string email);
        Task<Token> Login(UserLoginDto userLoginDto);
    }
}
