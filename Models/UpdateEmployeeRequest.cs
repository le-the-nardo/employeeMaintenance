namespace EmployeeMaintenance.Models;

public record UpdateEmployeeRequest(string FirstName, string LastName, Guid DepartmentId, string Phone, string Address);