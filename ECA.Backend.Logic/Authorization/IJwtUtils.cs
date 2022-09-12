using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECA.Backend.Common.Models;

namespace ECA.Backend.Logic.Authorization
{
    public interface IJwtUtils
    {
        public string GenerateJwtToken(UserAccount user);
        public int? ValidateJwtToken(string token);
        public RefreshToken GenerateRefreshToken(string ipAddress);
    }
}
