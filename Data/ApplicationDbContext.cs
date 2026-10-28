using Microsoft.EntityFrameworkCore;
using SmartCollegeMS.Models;

namespace SmartCollegeMS.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Student> Students { get; set; }
    public DbSet<Faculty> Faculties { get; set; }
    public DbSet<Attendance> Attendances { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // 1. Make StudentId Unique
        modelBuilder.Entity<Student>()
            .HasIndex(s => s.StudentId)
            .IsUnique();

        // 2. Make FacultyId Unique
        modelBuilder.Entity<Faculty>()
            .HasIndex(f => f.FacultyId)
            .IsUnique();

        // 3. Prevent duplicate attendance (1 student cannot have 2 records on the same date)
        modelBuilder.Entity<Attendance>()
            .HasIndex(a => new { a.StudentId, a.Date })
            .IsUnique();

        // 4. Relationship: Attendance -> Student (Prevent cascade delete)
        modelBuilder.Entity<Attendance>()
            .HasOne(a => a.Student)
            .WithMany()
            .HasForeignKey(a => a.StudentId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}