using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartCollegeMS.Data;
using SmartCollegeMS.Models;
using SmartCollegeMS.Models.ViewModels;

namespace SmartCollegeMS.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var today = DateTime.Today;
            
            var model = new DashboardViewModel
            {
                TotalStudents = await _context.Students.CountAsync(),
                TotalFaculty = await _context.Faculties.CountAsync(),
            };

            // Today's Attendance
            var todayAttendance = await _context.Attendances
                .Where(a => a.Date.Date == today)
                .ToListAsync();

            model.PresentToday = todayAttendance.Count(a => a.IsPresent);
            model.AbsentToday = todayAttendance.Count(a => !a.IsPresent);

            if (todayAttendance.Any())
            {
                model.AttendancePercentageToday = Math.Round((double)model.PresentToday / todayAttendance.Count * 100, 1);
            }

            // Last 7 Days Data for Chart.js
            for (int i = 6; i >= 0; i--)
            {
                var date = today.AddDays(-i);
                var dayAttendance = await _context.Attendances
                    .Where(a => a.Date.Date == date)
                    .ToListAsync();

                model.ChartLabels.Add(date.ToString("dd MMM"));
                model.ChartDataPresent.Add(dayAttendance.Count(a => a.IsPresent));
                model.ChartDataAbsent.Add(dayAttendance.Count(a => !a.IsPresent));
            }

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}