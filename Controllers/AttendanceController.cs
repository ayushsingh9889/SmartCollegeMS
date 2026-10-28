using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartCollegeMS.Data;
using SmartCollegeMS.Models;
using SmartCollegeMS.Models.ViewModels;

namespace SmartCollegeMS.Controllers
{
    public class AttendanceController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AttendanceController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Attendance?date=yyyy-mm-dd
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

            // Remove existing records for this date to allow editing
            var existingRecords = await _context.Attendances
                .Where(a => a.Date.Date == targetDate)
                .ToListAsync();
            
            if (existingRecords.Any())
            {
                _context.Attendances.RemoveRange(existingRecords);
            }

            // Insert the new records
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
    }
}