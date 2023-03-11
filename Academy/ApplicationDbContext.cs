using Academy.Models;
using Microsoft.EntityFrameworkCore;

namespace Academy;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }
    
    public DbSet<Student> Students { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<Class> Classes { get; set; }
    public DbSet<Professor> Professors { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // var connectionString = 
        // optionsBuilder
        //     .UseMySql()
    }
}