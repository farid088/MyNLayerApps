
using Microsoft.EntityFrameworkCore;
using MyNLayerApps.Entity.Entity;

namespace MyNLayerApps.DataAccess.Context;

public class MyNLayerAppsContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=.\SQlEXPRESS;Database=MyNLayerAppsDb;Trusted_Connection=true;TrustServerCertificate=true;");
        base.OnConfiguring(optionsBuilder);
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MyNLayerAppsContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
    DbSet<Department> Departments { get; set; }
    DbSet<Employee> Employees { get; set; }
    DbSet<Project> Projects { get; set; }
    DbSet<EmployeeProject> EmployeeProjects { get; set; }

}
