using MyNLayerApps.Entity.Entity.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyNLayerApps.Entity.Entity;

public class EmployeeProject : BaseEntity
{
    [NotMapped]
    public override int Id { get=> base.Id; set=> base.Id = value; }
    public int EmployeeId { get; set; }
    public Employee Employee { get; set; } = null!;
    public int ProjectId { get; set; }
    public Project Project { get; set; } = null!;
}
