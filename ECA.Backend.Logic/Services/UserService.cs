using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECA.Backend.Common.Models;
using ECA.Backend.Common.Models.Context;
using ECA.Backend.Logic.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ECA.Backend.Logic.Services
{
    public class UserService : IUserService
    {
        private readonly UsersContext _usersContext;
        private readonly IPasswordService _passwordService;
        private readonly ILogger _log;

        public UserService(UsersContext usersContext, IPasswordService passwordService, ILoggerFactory loggerFactory)
        {
            _usersContext = usersContext;
            _passwordService = passwordService;
            _log = loggerFactory.CreateLogger<UserService>();
        }

        public async Task<User> CreateUser(User user)
        {
            _log.LogInformation($"Create new user: {user.Username}");
            user.Password = _passwordService.Hash(user.Password);
            user.CreatedDate = DateTime.Now.ToString("o", CultureInfo.InvariantCulture);

            if (_usersContext.Users == null) return null;
            _usersContext.Users.Add(user);
            await _usersContext.SaveChangesAsync();

            return user;
        }
    }
}
