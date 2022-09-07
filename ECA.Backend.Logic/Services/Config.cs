using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECA.Backend.Logic.Interfaces;

namespace ECA.Backend.Logic.Services
{
    public class Config : IConfig
    {
        public string Database { get; set; }
    }
}
