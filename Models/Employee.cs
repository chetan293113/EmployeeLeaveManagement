using System.ComponentModel.DataAnnotations;

namespace EmployeeLeaveManagement.Models
{
    public class Employee
    {
       
        
            public int Id { get; set; }

            [Required]
            public string Name { get; set; }

            [Required]
            public string Email { get; set; }

            public string Department { get; set; }

            public ICollection<LeaveRequest>? LeaveRequests { get; set; }
        }
    }

