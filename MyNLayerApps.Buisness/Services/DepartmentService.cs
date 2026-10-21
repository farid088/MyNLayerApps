

using MyNLayerApps.DataAccess.Context;
using MyNLayerApps.Entity.Entity;
using Microsoft.EntityFrameworkCore;

namespace MyNLayerApps.Buisness.Services;

public class DepartmentService
{
    private readonly MyNLayerAppsContext _context;
    public DepartmentService(MyNLayerAppsContext context)
    {
        _context = context;
    }
    public async Task<List<Department>> GetAllDepartmentsAsync()
    {
        return await _context.Departments.ToListAsync();
    }

}
