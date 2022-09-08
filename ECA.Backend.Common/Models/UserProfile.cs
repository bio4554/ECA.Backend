using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ECA.Backend.Common.Models
{
    public class UserProfile
    {
        [Key]
        public Guid Id { get; set; }
        public Guid UserAccountId { get; set; }

        [JsonIgnore]
        public UserAccount UserAccount { get; set; }
        public string Description { get; set; }
    }
}
