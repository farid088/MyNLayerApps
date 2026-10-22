using MyNLayerApps.DataAccess.Context;
using MyNLayerApps.Entity.Entity;
using Microsoft.EntityFrameworkCore;
using MyNLayerApps.Buisness.Services.Interfaces;
using MyNLayerApps.Buisness.Dtos;
using MyNLayerApps.Buisness.Mappers;
namespace MyNLayerApps.Buisness.Services.Implementations;

public class DepartmentService : IDepartmentService
{
    private readonly MyNLayerAppsContext _context;
    public DepartmentService(MyNLayerAppsContext context) => _context = context;
    public async Task<List<DepartmentReturnDto>> GetAllDepartmentsAsync() =>
        await _context.Departments
        .AsNoTracking()
        .Select(d => MapperProfile.DepartmentToDepartmentReturnDto(d))
        .ToListAsync();

    public async Task<DepartmentReturnDto> GetDepartmentByIdAsync(int id)
    {
        if (id <= 0)
        {
            throw new ArgumentException("Id must be greater than zero.", nameof(id));
        }
        var department = await _context.Departments.FindAsync(id);
        if (department == null)
        {
            throw new KeyNotFoundException($"Department with Id {id} not found.");
        }

        var departmentReturnDto=MapperProfile.DepartmentToDepartmentReturnDto(department);
        return departmentReturnDto;
    }

    public async Task<Department> CreateDepartmentAsync(DepartmentCreateDto department)
    {
        if (department == null)
        {
            throw new ArgumentNullException(nameof(department), "Department cannot be null.");
        }

        var existingDepartment = await _context.Departments.FirstOrDefaultAsync(d => d.Name.ToLower() == department.Name.ToLower());
        if (existingDepartment != null)
        {
            throw new InvalidOperationException($"A department with the name '{department.Name}' already exists.");
        }
        var newDepartment = new Department
        {
            Name = department.Name,
            Description = department.Description,
            Location = department.Location,
            Limit = department.Limit
        };
        _context.Departments?.AddAsync(newDepartment);
        await _context.SaveChangesAsync();
        return newDepartment;
    }
    public async Task<Department> UpdateDepartmentAsync(Department department)
    {
        if (department == null)
        {
            throw new ArgumentNullException(nameof(department), "Department cannot be null.");
        }
        var existingDepartment = await _context.Departments.FindAsync(department.Id);
        if (existingDepartment == null)
        {
            throw new KeyNotFoundException($"Department with Id {department.Id} not found.");
        }
        existingDepartment.Name = department.Name;
        existingDepartment.Description = department.Description;
        _context.Departments.Update(existingDepartment);
        await _context.SaveChangesAsync();
        return existingDepartment;
    }
    public async Task DeleteDepartmentAsync(int id)
    {
     var departmentReturnDto = await GetDepartmentByIdAsync(id);
        
        var department = MapperProfile.DepartmentReturnDtoToDepartment(departmentReturnDto);
        _context.Departments.Remove(department);
        await _context.SaveChangesAsync();
    }
}