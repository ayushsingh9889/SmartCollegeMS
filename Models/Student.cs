using System.ComponentModel.DataAnnotations;

namespace SmartCollegeMS.Models;

public class Student
{
    public int Id { get; set; } // Primary Key

    [Required(ErrorMessage = "Student ID is required")]
    [StringLength(20)]
    [Display(Name = "Student ID")]
    public string StudentId { get; set; } = string.Empty;

    [Required(ErrorMessage = "Full Name is required")]
    [StringLength(100)]
    [Display(Name = "Full Name")]
    public string FullName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid Email Address")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Phone is required")]
    [Phone(ErrorMessage = "Invalid Phone Number")]
    public string Phone { get; set; } = string.Empty;

    [Required(ErrorMessage = "Department is required")]
    public string Department { get; set; } = string.Empty;

    [Required(ErrorMessage = "Semester is required")]
    [Range(1, 8, ErrorMessage = "Semester must be between 1 and 8")]
    public int Semester { get; set; }

    [DataType(DataType.Date)]
    [Display(Name = "Registration Date")]
    public DateTime RegistrationDate { get; set; } = DateTime.Now;
}