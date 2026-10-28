using System.ComponentModel.DataAnnotations;

namespace SmartCollegeMS.Models.ViewModels
{
    public class AttendanceViewModel
    {
        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; } = DateTime.Today;

        public List<StudentAttendanceRecord> Students { get; set; } = new List<StudentAttendanceRecord>();
    }

    public class StudentAttendanceRecord
    {
        public int StudentId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string StudentCode { get; set; } = string.Empty;
        public bool IsPresent { get; set; }
    }
}