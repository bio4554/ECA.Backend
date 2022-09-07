using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECA.Backend.Common.Models
{
    public class UserData
    {
        [Key]
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Description { get; set; }
    }
}
