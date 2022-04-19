using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class Education
    {
        [Key, Required]
        public int Id { get; set; }
        [Required]
        public string Degree { get; set; }
        [Required]
        public string GPA { get; set; }
        [Required]
        public int UniversityId { get; set; }

        [JsonIgnore]
        public virtual ICollection<Profiling> Profiling { get; set; }

        [JsonIgnore]
        public virtual University University { get; set; }
    }
}
