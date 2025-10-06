using Ex5.Models;
using Ex5.Services;

namespace Ex5Tests
{
    [TestFixture]
    public class VacationsServiceTests
    {
        [Test]
        /* 
         * Test to verify if an employee can request vacation based on their granted days and already taken vacations.
         */
        public void employee_can_request_vacation()
        {
            /* 
             * Setting up test data 
             */
            var employee = new Employee
            {
                Id = 1,
                Name = "John Doe",
                TeamId = 1,
                PositionId = 1,
                VacationPackageId = 1
            };
            var vacationPackage = new VacationPackage
            {
                Id = 1,
                Name = "Standard Package",
                GrantedDays = 20,
                Year = DateTime.Now.Year
            };

            var vacations = new List<Vacations>
            {
                new Vacations
                {
                    Id = 1,
                    DateSince = new DateTime(DateTime.Now.Year, 1, 1),
                    DateUntil = new DateTime(DateTime.Now.Year, 1, 10),
                    NumberOfHours = 80,
                    IsPartialVacation = false,
                    EmployeeId = 1
                }
            };
            /* 
             * End of test data setup 
             */

            /* 
             * Initialize the service to be tested
             */
            var vacationsService = new VacationsService();

            /*
             * Check if the employee can request more vacation days
             */
            var canRequest = vacationsService.IfEmployeeCanRequestVacation(employee, vacations, vacationPackage);

            /*
             * Employee has 10 days left (20 granted - 10 used), so they can request more vacation
             */
            Assert.That(canRequest, Is.True);
        }

        [Test]
        /*
         * Test to verify if an employee cannot request vacation when they have exhausted their granted days.
         */
        public void employee_cant_request_vacation()
        {
            /* 
             * Setting up test data 
             */
            var employee = new Employee
            {
                Id = 1,
                Name = "John Doe",
                TeamId = 1,
                PositionId = 1,
                VacationPackageId = 1
            };
            var vacationPackage = new VacationPackage
            {
                Id = 1,
                Name = "Standard Package",
                GrantedDays = 5,
                Year = DateTime.Now.Year
            };

            var vacations = new List<Vacations>
            {
                new Vacations
                {
                    Id = 1,
                    DateSince = new DateTime(DateTime.Now.Year, 1, 1),
                    DateUntil = new DateTime(DateTime.Now.Year, 1, 5),
                    NumberOfHours = 40,
                    IsPartialVacation = false,
                    EmployeeId = 1
                }
            };
            /* 
             * End of test data setup 
             */

            /*
             * Initialize the service to be tested
             */
            var vacationsService = new VacationsService();

            /*
             * Check if the employee can request more vacation days
             */
            var canRequest = vacationsService.IfEmployeeCanRequestVacation(employee, vacations, vacationPackage);

            /*
             * Employee has 0 days left (5 granted - 5 used), so they cannot request more vacation
             */
            Assert.That(canRequest, Is.False);
        }
    }
}
