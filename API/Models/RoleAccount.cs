using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class RoleAccount
    {
      
        public string NIK { get; set; }
        public int RoleId { get; set; }

        [JsonIgnore]
        public virtual Account Account { get; set; }

        [JsonIgnore]
        public virtual Role Role { get; set; }  

    }
}
