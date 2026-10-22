
using Microsoft.EntityFrameworkCore;
using MyNLayerApps.Entity.Entity;
using MyNLayerApps.Entity.Entity.Common;
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
    public override int SaveChanges()
    {
        var datas = ChangeTracker.Entries<AuditEntity>();
        foreach (var data in datas)
        {
            switch(data.State)
            {
                case EntityState.Added:
                    data.Entity.CreatedAt = DateTime.Now;
                    break;
                case EntityState.Modified:
                    data.Entity.UpdatedAt = DateTime.Now;
                    break;
                default:
                    break;
            }
        }
        return base.SaveChanges();

    }
    public DbSet<Department> Departments { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<EmployeeProject> EmployeeProjects { get; set; }

}
