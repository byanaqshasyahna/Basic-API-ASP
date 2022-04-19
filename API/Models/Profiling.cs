using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class Profiling
    {

        [Key]
        public string NIK { get; set; }

        [Required]
        public int EducationId { get; set; }

        [JsonIgnore]
        public virtual Account Account { get; set; }
        [JsonIgnore]
        public virtual Education Education { get; set; }

    }
}
