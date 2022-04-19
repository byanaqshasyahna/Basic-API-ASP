using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class Role
    {
        [Key, Required]
        public int Id { get; set; }
        public string RoleName { get; set; }

        [JsonIgnore]
        public virtual ICollection<RoleAccount> RoleAccounts { get; set; }
        
    }
}
