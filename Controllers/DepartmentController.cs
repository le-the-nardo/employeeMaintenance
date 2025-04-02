using EmployeeMaintenance.Data;
using EmployeeMaintenance.Entities;
using EmployeeMaintenance.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeMaintenance.Controllers;

public static class DepartmentController
{
    public static void AddDepartmentsRoutes(this WebApplication app)
    {
        var departmentRoutes = app.MapGroup("department");

        departmentRoutes.MapGet("all", async (AppDbContext context) =>
        {
            var departments = await context.Departments.ToListAsync();
            return departments;
        });

        departmentRoutes.MapPost("", async (AddDepartmentRequest request, AppDbContext context) =>
        {
            var newDepartment = new Department(request.DepartmentName);

            await context.Departments.AddAsync(newDepartment);
            await context.SaveChangesAsync();

        });
    }
    
}