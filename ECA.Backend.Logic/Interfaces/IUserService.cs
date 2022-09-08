using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECA.Backend.Common.Models;
using ECA.Backend.Common.RequestResponse;

namespace ECA.Backend.Logic.Interfaces
{
    public interface IUserService
    {
        public Task<UserAccount> CreateUser(UserAccount userAccount);
        public Task<LoginResponse> Login(LoginRequest req);
    }
}
