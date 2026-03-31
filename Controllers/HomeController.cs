using Microsoft.AspNetCore.Mvc;
using EmployeeLeaveManagement.Data;

namespace EmployeeLeaveManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            ViewBag.Employees = _context.Employees.ToList();
            ViewBag.LeaveRequests = _context.LeaveRequests.ToList();

            return View();
        }
    }
}