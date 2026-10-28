using System.ComponentModel.DataAnnotations;

namespace SmartCollegeMS.Models;

public class Faculty
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Faculty ID is required")]
    [StringLength(20)]
    [Display(Name = "Faculty ID")]
    public string FacultyId { get; set; } = string.Empty;

    [Required(ErrorMessage = "Name is required")]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Department is required")]
    public string Department { get; set; } = string.Empty;

    [Required(ErrorMessage = "Phone is required")]
    [Phone]
    public string Phone { get; set; } = string.Empty;
}