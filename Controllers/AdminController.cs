using EmployeeLeaveManagement.Data;
using EmployeeLeaveManagement.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EmployeeLeaveManagement.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(AdminLoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            if (model.Username == "admin" && model.Password == "admin123")
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, model.Username),
                    new Claim(ClaimTypes.Role, "Admin")
                };

                var identity = new ClaimsIdentity(
                    claims,
                    CookieAuthenticationDefaults.AuthenticationScheme);

                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    principal);

                return RedirectToAction("Index");
            }

            ViewBag.Error = "Invalid username or password";
            return View(model);
        }

        [Authorize]
        public IActionResult Index()
        {
            ViewBag.TotalEmployees = _context.Employees.Count();
            ViewBag.PendingLeaves = _context.LeaveRequests.Count(x => x.Status == "Pending");
            ViewBag.ApprovedLeaves = _context.LeaveRequests.Count(x => x.Status == "Approved");
            ViewBag.RejectedLeaves = _context.LeaveRequests.Count(x => x.Status == "Rejected");
            ViewBag.LeaveRequests = _context.LeaveRequests.ToList();

            return View();
        }

        [Authorize]
        public IActionResult Approve(int id)
        {
            var leave = _context.LeaveRequests.FirstOrDefault(x => x.Id == id);

            if (leave != null)
            {
                leave.Status = "Approved";
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [Authorize]
        public IActionResult Reject(int id)
        {
            var leave = _context.LeaveRequests.FirstOrDefault(x => x.Id == id);

            if (leave != null)
            {
                leave.Status = "Rejected";
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Login");
        }
    }
}