using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartCollegeMS.Models;

public class Attendance
{
    public int Id { get; set; }

    [Required]
    public int StudentId { get; set; } // Foreign Key

    [ForeignKey("StudentId")]
    public Student? Student { get; set; } // Navigation property

    [Required]
    [DataType(DataType.Date)]
    public DateTime Date { get; set; } = DateTime.Today;

    [Required]
    public bool IsPresent { get; set; }
}