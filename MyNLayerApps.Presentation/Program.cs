using Microsoft.Extensions.DependencyInjection;
using MyNLayerApps.Buisness.Services.Implementations;
using MyNLayerApps.Buisness.Services.Interfaces;
using MyNLayerApps.DataAccess.Context;


var serviceCollection = new ServiceCollection();
serviceCollection.AddDbContext<MyNLayerAppsContext>();
serviceCollection.AddScoped<DepartmentService>();
serviceCollection.AddScoped<EmployeeService>();


var serviceProvider = serviceCollection.BuildServiceProvider();
var departmentService = serviceProvider.GetRequiredService<DepartmentService>();
