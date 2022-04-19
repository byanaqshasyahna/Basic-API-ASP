using System;
using System.ComponentModel.DataAnnotations;

namespace API.VirtualModel
{
    public class RegisterVM
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Gender { get; set; }
        public string Phone { get; set; }
        public DateTime BirthDate { get; set; }
        public int Salary { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        [Required]
        public string Degree { get; set; }
        [Required]
        public string GPA { get; set; }

        public int UniversityId { get; set; }

    }

}
