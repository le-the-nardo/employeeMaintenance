namespace EmployeeMaintenance.Models;

public record AddEmployeeRequest(string FirstName, string LastName, Guid DepartmentId, string Phone, string Address);
