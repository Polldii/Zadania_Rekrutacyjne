using Ex1.Models;

namespace Ex1.Services
{
    public interface IEmployeeStructureService
    {
        /* 
         * Fills the employee structure list based on the provided employees
         */
        List<EmployeeStructure> FillEmployeesStructure(List<Employee> employees);
        /* 
         * Gets the row number of a superior for a given employee
         */
        int? GetSuperiorRowOfEmployee(int employeeId, int superiorId);
    }
}
