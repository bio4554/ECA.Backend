using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECA.Backend.Common.Models;

namespace ECA.Backend.Common.RequestResponse
{
    public class LoginResponse
    {
        public bool Authenticated { get; set; }
        public UserAccount? Account { get; set; }
        public bool UnknownUser { get; set; }
    }
}
