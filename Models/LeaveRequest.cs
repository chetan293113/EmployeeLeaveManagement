using System.ComponentModel.DataAnnotations;

namespace EmployeeLeaveManagement.Models
{
    public class LeaveRequest
    {
        public int Id { get; set; }

        [Required]
        public string EmployeeName { get; set; }

        [Required]
        public string LeaveType { get; set; }

        [Required]
        public DateTime FromDate { get; set; }

        [Required]
        public DateTime ToDate { get; set; }

        public string Status { get; set; } = "Pending";
    }
}