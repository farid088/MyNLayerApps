using Microsoft.EntityFrameworkCore;
using MyNLayerApps.Buisness.Services.Interfaces;
using MyNLayerApps.DataAccess.Context;
using MyNLayerApps.Entity.Entity;

namespace MyNLayerApps.Buisness.Services.Implementations;

public class EmployeeService : IEmployeeService
{
    private readonly MyNLayerAppsContext _context;

    public EmployeeService(MyNLayerAppsContext context) => _context = context;

    public async Task<List<Employee>> GetAllEmployeesAsync() =>
        await _context.Employees.ToListAsync();

    public async Task<Employee?> GetEmployeeByIdAsync(int id)
    {
        if (id <= 0)
        {
            throw new ArgumentException("Id must be greater than zero.", nameof(id));
        }

        var employee = await _context.Employees.FindAsync(id);
        if (employee == null)
        {
            throw new KeyNotFoundException($"Employee with Id {id} not found.");
        }

        return employee;
    }
    public async Task<Employee> CreateEmployeeAsync(Employee employee)
    {
        if (employee == null)
        {
            throw new ArgumentNullException(nameof(employee), "Employee cannot be null.");
        }

        // 1. Email təkrarlanması yoxlanılır
        var emailExists = await _context.Employees
            .AnyAsync(e => e.Email.ToLower() == employee.Email.ToLower());

        if (emailExists)
        {
            throw new InvalidOperationException($"An employee with the email '{employee.Email}' already exists.");
        }

        // 2. Departamentin varlığı yoxlanılır
        var department = await _context.Departments
            .FirstOrDefaultAsync(d => d.Id == employee.DepartmentId);

        if (department == null)
        {
            throw new KeyNotFoundException($"Department with Id {employee.DepartmentId} not found.");
        }

        // 3. Departamentin limiti yoxlanılır (Şəkildəki kimi)
        var departmentEmployeeCount = await _context.Employees
            .CountAsync(e => e.DepartmentId == employee.DepartmentId);

        if (departmentEmployeeCount >= department.Limit)
        {
            throw new InvalidOperationException($"Department with Id {employee.DepartmentId} has reached its limit.");
        }

        // 4. Əlavə olunur
        _context.Employees.Add(employee);
        await _context.SaveChangesAsync();

        return employee;
    }

    public async Task<Employee> UpdateEmployeeAsync(Employee employee)
    {
        if (employee == null)
        {
            throw new ArgumentNullException(nameof(employee), "Employee cannot be null.");
        }

        var existingEmployee = await _context.Employees.FindAsync(employee.Id);
        if (existingEmployee == null)
        {
            throw new KeyNotFoundException($"Employee with Id {employee.Id} not found.");
        }
        if (existingEmployee.DepartmentId != employee.DepartmentId)
        {
            var department = await _context.Departments
                .FirstOrDefaultAsync(d => d.Id == employee.DepartmentId);
            if (department == null)
            {
                throw new KeyNotFoundException($"Department with Id {employee.DepartmentId} not found.");
            }
            var departmentEmployeeCount = await _context.Employees
                .CountAsync(e => e.DepartmentId == employee.DepartmentId);
            if (departmentEmployeeCount >= department.Limit)
            {
                throw new InvalidOperationException($"Department with Id {employee.DepartmentId} has reached its limit.");
            }
        }
        var duplicateEmail = await _context.Employees
            .AnyAsync(e => e.Id != employee.Id && e.Email.ToLower() == employee.Email.ToLower());
        if (duplicateEmail)
        {
            throw new InvalidOperationException($"An employee with the email '{employee.Email}' already exists.");
        }

        var departmentExists = await _context.Departments
            .AnyAsync(d => d.Id == employee.DepartmentId);
        if (!departmentExists)
        {
            throw new KeyNotFoundException($"Department with Id {employee.DepartmentId} not found.");
        }

        existingEmployee.FirstName = employee.FirstName;
        existingEmployee.LastName = employee.LastName;
        existingEmployee.Email = employee.Email;
        existingEmployee.PhoneNumber = employee.PhoneNumber;
        existingEmployee.HireDate = employee.HireDate;
        existingEmployee.EmployeeStatus = employee.EmployeeStatus;
        existingEmployee.DepartmentId = employee.DepartmentId;

        await _context.SaveChangesAsync();
        return existingEmployee;
    }

    public async Task DeleteEmployeeAsync(int id)
    {
        if (id <= 0)
        {
            throw new ArgumentException("Id must be greater than zero.", nameof(id));
        }

        var employee = await _context.Employees.FindAsync(id);
        if (employee == null)
        {
            throw new KeyNotFoundException($"Employee with Id {id} not found.");
        }

        _context.Employees.Remove(employee);
        await _context.SaveChangesAsync();
    }
}
