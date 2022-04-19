using System.Collections.Generic;

namespace API.Models
{
    public class EmployeeResult
    {
      
        public int Status { get; set; }
        public List<Employee> Result { get; set; } = new List<Employee>();
        public string Message { get; set; }
        

    }
}
