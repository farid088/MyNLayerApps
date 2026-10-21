
using MyNLayerApps.Entity.Entity.Common;
using MyNLayerApps.Entity.Enum;

namespace MyNLayerApps.Entity.Entity;

public class Employee: AuditEntity
{
    public string FirstName { get; set; } = null!;
    public string? LastName { get; set; }
    public string Email { get; set; } = null!;
    public string? PhoneNumber { get; set; }
    public DateTime HireDate { get; set; }
    public EmployeeStatus EmployeeStatus { get; set; }
    public int DepartmentId { get; set; }
    public Department Department { get; set; } = null!;
    public ICollection<EmployeeProject> EmployeeProjects { get; set; } = new List<EmployeeProject>();
    
}
