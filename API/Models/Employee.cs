using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("Employee")]
    public class Employee
    {
        [Key]
        //[Required]
        public string NIK { get; set; }

        [Required]
        public string Firstname { get; set; }

        [Required]
        public string Lastname { get; set; }  
        
       
        public string Phone { get; set; }   

        public DateTime BirthDate { get; set; } 

        public int Salary { get; set; }
        
        public string Email { get; set; }   
        
        public Gender Gender { get; set; }

        [JsonIgnore]
        public virtual Account Account { get; set; }

    }

    public enum Gender
    {
        Male,
        Female
    }
}
