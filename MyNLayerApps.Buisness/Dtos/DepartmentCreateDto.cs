
namespace MyNLayerApps.Buisness.Dtos;

public class DepartmentCreateDto
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public string? Location { get; set; }
    public int Limit { get; set; }
}
