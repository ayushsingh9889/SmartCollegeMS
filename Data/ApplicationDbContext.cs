using Microsoft.EntityFrameworkCore;

namespace SmartCollegeMS.Data;

/// <summary>
/// EF Core database context for the Smart College Management System.
/// Each DbSet below will become a table in MySQL once we add models.
/// </summary>
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    // DbSets (tables) will be added here in Phase 4.
}