using Ex1.Models;
using Ex1.Services;
using Microsoft.Extensions.DependencyInjection;

class Program
{
       static void Main()
    {
        /* 
         * Set up dependency injection 
         */
        var serviceProvider = new ServiceCollection()
            .AddSingleton<IEmployeeStructureService, EmployeeStructureService>()
            .BuildServiceProvider();

        /* 
         * Get the service
         */
        var employeeStructureService = serviceProvider.GetRequiredService<IEmployeeStructureService>();

        /* 
         * Sample employees
         */
        var employees = new List<Employee>
        {
            new Employee { Id = 1, Name = "Jan Kowalski", SuperiorId = null },
            new Employee { Id = 2, Name = "Kamil Nowak", SuperiorId = 1 },
            new Employee { Id = 3, Name = "Andrzej Abacki", SuperiorId = 1 },
            new Employee { Id = 4, Name = "Piotr Zieliński", SuperiorId = 2 },
        };

        /* 
         * Fill the employee structure
         */
        employeeStructureService.FillEmployeesStructure(employees);

        /* 
         * Test cases
         */
        Console.WriteLine("row1 = " + employeeStructureService.GetSuperiorRowOfEmployee(2,1));
        Console.WriteLine("row2 = " + employeeStructureService.GetSuperiorRowOfEmployee(4,3));
        Console.WriteLine("row3 = " + employeeStructureService.GetSuperiorRowOfEmployee(4,1));

    }
}
