using System.ComponentModel.DataAnnotations;

namespace EmployeeLeaveManagement.Models
{
    public class AdminLoginViewModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}