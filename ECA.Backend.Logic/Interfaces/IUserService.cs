﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECA.Backend.Common.Models;

namespace ECA.Backend.Logic.Interfaces
{
    public interface IUserService
    {
        public Task<User> CreateUser(User user);
    }
}
