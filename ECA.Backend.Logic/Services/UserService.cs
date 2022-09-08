using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECA.Backend.Common.Models;
using ECA.Backend.Common.RequestResponse;
using ECA.Backend.Data.Context;
using ECA.Backend.Logic.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ECA.Backend.Logic.Services
{
    public class UserService : IUserService
    {
        private readonly BackendContext _dataContext;
        private readonly IPasswordService _passwordService;
        private readonly ILogger _log;

        public UserService(BackendContext usersContext, IPasswordService passwordService, ILoggerFactory loggerFactory)
        {
            _dataContext = usersContext;
            _passwordService = passwordService;
            _log = loggerFactory.CreateLogger<UserService>();
        }

        public async Task<UserAccount> CreateUser(UserAccount userAccount)
        {
            _log.LogInformation($"Create new userAccount: {userAccount.Username}");
            


            userAccount.Password = _passwordService.Hash(userAccount.Password);
            userAccount.Email ??= string.Empty;
            userAccount.CreatedDate = DateTime.Now.ToString("o", CultureInfo.InvariantCulture);
            userAccount.Profile = new UserProfile(){Description = string.Empty};

            if (_dataContext.Set<UserAccount>() == null) return null;
            _dataContext.Set<UserAccount>().Add(userAccount);
            await _dataContext.SaveChangesAsync();

            return userAccount;
        }

        public async Task<UserAccount> GetUserById(string id)
        {
            if (_dataContext.Set<UserAccount>() == null) return null;
            var user = await _dataContext.Set<UserAccount>().FindAsync(id);

            if (user == null) return null;

            return user;
        }

        public async Task<UserAccount?> GetUserByName(string username)
        {
            try
            {
                return await _dataContext.Set<UserAccount>().SingleAsync(u => u.Username == username);
            }
            catch(Exception e)
            {
                _log.LogError(e.Message);
                _log.LogError(e.StackTrace);
                return null;
            }
        }

        public async Task<LoginResponse> Login(LoginRequest req)
        {
            var user = await GetUserByName(req.Username);
            if (user == null) return new LoginResponse() { Authenticated = false, UnknownUser = true};
            var authenticated = _passwordService.Check(user.Password, req.Password);
            if (authenticated)
                return new LoginResponse() { Account = user, Authenticated = true, UnknownUser = false };
            return new LoginResponse() { Authenticated = false, UnknownUser = false };
        }
    }
}
