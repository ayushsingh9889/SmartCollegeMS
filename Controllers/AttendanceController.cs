using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartCollegeMS.Data;
using SmartCollegeMS.Models;
using SmartCollegeMS.Models.ViewModels;
using Microsoft.AspNetCore.Authorization; // Added for Security

namespace SmartCollegeMS.Controllers
{
    [Authorize] // Added for Security
    public class AttendanceController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AttendanceController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Attendance
        public async Task<IActionResult> Index(DateTime? date)
        {
            var targetDate = date ?? DateTime.Today;
            var students = await _context.Students.ToListAsync();
            var existingAttendance = await _context.Attendances
                .Where(a => a.Date.Date == targetDate.Date)
                .ToListAsync();

            var viewModel = new AttendanceViewModel
            {
                Date = targetDate,
                Students = students.Select(s => new StudentAttendanceRecord
                {
                    StudentId = s.Id,
                    FullName = s.FullName,
                    StudentCode = s.StudentId,
                    IsPresent = existingAttendance.FirstOrDefault(a => a.StudentId == s.Id)?.IsPresent ?? false
                }).ToList()
            };

            return View(viewModel);
        }

        // POST: Attendance/Save
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(AttendanceViewModel model)
        {
            if (model.Students == null || !model.Students.Any())
            {
                TempData["Error"] = "No students to save.";
                return RedirectToAction(nameof(Index), new { date = model.Date.ToString("yyyy-MM-dd") });
            }

            var targetDate = model.Date.Date;
            var existingRecords = await _context.Attendances
                .Where(a => a.Date.Date == targetDate)
                .ToListAsync();
            
            if (existingRecords.Any())
            {
                _context.Attendances.RemoveRange(existingRecords);
            }

            foreach (var studentRecord in model.Students)
            {
                var attendance = new Attendance
                {
                    StudentId = studentRecord.StudentId,
                    Date = targetDate,
                    IsPresent = studentRecord.IsPresent
                };
                _context.Attendances.Add(attendance);
            }

            await _context.SaveChangesAsync();

            TempData["Success"] = $"Attendance for {targetDate:dd-MMM-yyyy} saved successfully!";
            return RedirectToAction(nameof(Index), new { date = model.Date.ToString("yyyy-MM-dd") });
        }

        // GET: Attendance/Report
        public async Task<IActionResult> Report(DateTime? startDate, DateTime? endDate)
        {
            var start = startDate ?? DateTime.Today.AddDays(-7); 
            var end = endDate ?? DateTime.Today;

            ViewData["StartDate"] = start.ToString("yyyy-MM-dd");
            ViewData["EndDate"] = end.ToString("yyyy-MM-dd");

            var reportData = await _context.Attendances
                .Include(a => a.Student)
                .Where(a => a.Date.Date >= start.Date && a.Date.Date <= end.Date)
                .OrderByDescending(a => a.Date)
                .ThenBy(a => a.Student!.FullName) // Null-forgiving operator to fix warning
                .ToListAsync();

            return View(reportData);
        }

        // GET: Attendance/ExportCsv
        public async Task<IActionResult> ExportCsv(DateTime startDate, DateTime endDate)
        {
            var reportData = await _context.Attendances
                .Include(a => a.Student)
                .Where(a => a.Date.Date >= startDate.Date && a.Date.Date <= endDate.Date)
                .OrderByDescending(a => a.Date)
                .ThenBy(a => a.Student!.FullName) // Null-forgiving operator to fix warning
                .ToListAsync();

            var builder = new System.Text.StringBuilder();
            builder.AppendLine("Date,Student ID,Student Name,Department,Status");

            foreach (var record in reportData)
            {
                string status = record.IsPresent ? "Present" : "Absent";
                var student = record.Student!; // Null-forgiving operator to fix warning
                builder.AppendLine($"{record.Date:dd-MMM-yyyy},{student.StudentId},{student.FullName},{student.Department},{status}");
            }

            var bytes = System.Text.Encoding.UTF8.GetBytes(builder.ToString());
            return File(bytes, "text/csv", $"Attendance_Report_{startDate:yyyyMMdd}_to_{endDate:yyyyMMdd}.csv");
        }
    }
}