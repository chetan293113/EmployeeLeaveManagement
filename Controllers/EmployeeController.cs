using EmployeeLeaveManagement.Data;
using EmployeeLeaveManagement.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeLeaveManagement.Controllers
{
    public class EmployeeController : Controller
    {
       
            private readonly ApplicationDbContext _context;

            public EmployeeController(ApplicationDbContext context)
            {
                _context = context;
            }

            public IActionResult Index()
            {
                var employees = _context.Employees.ToList();
                return View(employees);
            }

            public IActionResult Create()
            {
                return View();
            }

            [HttpPost]
            public IActionResult Create(Employee employee)
            {
                if (ModelState.IsValid)
                {
                    _context.Employees.Add(employee);
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(employee);
            }
    }
}
