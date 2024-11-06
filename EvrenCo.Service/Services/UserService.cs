using EvrenCo.Core.DTOs;
using EvrenCo.Core.Models;
using EvrenCo.Core.Repositories;
using EvrenCo.Core.Services;
using EvrenCo.Core.UnitOfWorks;
using EvrenCo.Service.Hashing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvrenCo.Service.Services
{
    public class UserService : Service<User>, IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenHandler _tokenHandler;
        public UserService(IGenericRepository<User> repository, IUnitOfWorks unitOfWorks, IUserRepository userRepository, ITokenHandler tokenHandler) : base(repository, unitOfWorks)
        {
            _userRepository = userRepository;
            _tokenHandler = tokenHandler;
        }

        public User GetByEmail(string email)
        {
            User user = _userRepository.Where(x=> x.Email == email).FirstOrDefault();

            return user ?? user;
        }

        public async Task<Token> Login(UserLoginDto userLoginDto)
        {
            Token token = new Token();

            var user = GetByEmail(userLoginDto.Email);

            if (user == null)
            {
                return null;
            }

            var result = false;

            result = HashingHelper.VerifyPasswordHash(userLoginDto.Password,user.PasswordHash,user.PasswordSalt);

            

            //get roles TODO

            if (result)
            {
                var roles = user.Group.GroupInRoles.Select(x => x.Role).ToList();
                token = _tokenHandler.CreateToken(user, roles);
                return token;
            }

            return null;
        }
    }
}
