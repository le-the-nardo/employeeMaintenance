using EmployeeMaintenance.Data;
using EmployeeMaintenance.Entities;
using EmployeeMaintenance.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeMaintenance.Controllers;

public static class EmployeeController
{
    public static void AddEmployeeRoutes(this WebApplication app)
    {
        var employeeRoutes = app.MapGroup("employee");

        employeeRoutes.MapPost("", async (AddEmployeeRequest request, AppDbContext context) =>
        {
            var newEmployee = new Employee(request.FirstName, request.LastName, request.DepartmentId, request.Phone, request.Address);

            await context.Employess.AddAsync(newEmployee);
            await context.SaveChangesAsync();
        });

        employeeRoutes.MapGet("all", async (AppDbContext context) =>
        {
            var employees = await context.Employess.ToListAsync();
            return employees;
        });

        employeeRoutes.MapGet("{id:guid}", (Guid id, AppDbContext context) =>
        {
            var employee = context.Employess.SingleOrDefaultAsync(e => e.Id == id);
            return employee;
        });

        employeeRoutes.MapPut("{id:guid}", async (Guid id, UpdateEmployeeRequest request, AppDbContext context) =>
        {
            var employee = await context.Employess.SingleOrDefaultAsync(e => e.Id == id);

            if (employee == null)
                return Results.NotFound();
            
            employee.UpdateEmployee(request.FirstName, request.LastName, request.DepartmentId, request.Phone, request.Address);

            await context.SaveChangesAsync();
            return Results.Ok(employee);
        });

        employeeRoutes.MapDelete("{id:guid}", async (Guid id, AppDbContext context) =>
        {
            var employee = await context.Employess.SingleOrDefaultAsync(e => e.Id == id);

            if (employee == null)
                return Results.NotFound();

            context.Employess.Remove(employee);
            await context.SaveChangesAsync();

            return Results.Ok($"Employee Id '{id}' removed successfully");
        });
    }
}