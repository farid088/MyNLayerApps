
using Microsoft.EntityFrameworkCore;

namespace MyNLayerApps.DataAccess.Context;

public class MyNLayerAppsContext: DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=.\SQlEXPRESS;Database=MyNLayerAppsDb;Trusted_Connection=true;TrustServerCertificate=true;");
        base.OnConfiguring(optionsBuilder);
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        base.OnModelCreating(modelBuilder);
    }
}
