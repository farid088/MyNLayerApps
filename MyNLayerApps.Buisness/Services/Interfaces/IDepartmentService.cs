using MyNLayerApps.Buisness.Dtos;
using MyNLayerApps.Entity.Entity;

namespace MyNLayerApps.Buisness.Services.Interfaces
{
    public interface IDepartmentService
    {
        Task<Department> CreateDepartmentAsync(DepartmentCreateDto department);
        Task DeleteDepartmentAsync(int id);
        Task<List<DepartmentReturnDto>> GetAllDepartmentsAsync();
        Task<DepartmentReturnDto> GetDepartmentByIdAsync(int? id);
        Task<Department> UpdateDepartmentAsync(Department department);
    }
}