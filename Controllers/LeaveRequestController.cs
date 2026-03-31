using Microsoft.AspNetCore.Mvc;
using EmployeeLeaveManagement.Data;
using EmployeeLeaveManagement.Models;

namespace EmployeeLeaveManagement.Controllers
{
    public class LeaveRequestController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LeaveRequestController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var leaves = _context.LeaveRequests.ToList();
            return View(leaves);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(LeaveRequest leave)
        {
            if (ModelState.IsValid)
            {
                _context.LeaveRequests.Add(leave);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(leave);
        }
    }
}