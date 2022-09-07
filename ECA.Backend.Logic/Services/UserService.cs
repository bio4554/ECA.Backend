using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECA.Backend.Common.Models;
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

        public async Task<User> CreateUser(User user)
        {
            _log.LogInformation($"Create new user: {user.Username}");
            user.Password = _passwordService.Hash(user.Password);
            user.Email ??= string.Empty;
            user.CreatedDate = DateTime.Now.ToString("o", CultureInfo.InvariantCulture);

            if (_dataContext.Set<User>() == null) return null;
            _dataContext.Set<User>().Add(user);
            await _dataContext.SaveChangesAsync();

            return user;
        }

        public async Task<User> GetUser(string id)
        {
            if (_dataContext.Set<User>() == null) return null;
            var user = await _dataContext.Set<User>().FindAsync(id);

            if (user == null) return null;

            return user;
        }
    }
}
