using Ex5.Models;

namespace Ex5.Services
{
    public class VacationsService
    {
        /* 
         * This method calculates the number of free vacation days an employee has left for the current year.
         It takes into account the vacations already taken by the employee and the total granted days from their vacation package.
         Parameters:
         - employee: The employee for whom to calculate free vacation days.
         - vacations: A list of all vacations taken by employees.
         - vacationPackage: The vacation package assigned to the employee, which includes the total granted days.
         Returns:
         - The number of free vacation days remaining for the employee in the current year.
        */
        public int CountFreeDaysForEmployee(Employee employee,List<Vacations> vacations,VacationPackage vacationPackage)
        {
            var currentYear = DateTime.Now.Year;

            var usedDays = vacations
                .Where(v => v.EmployeeId == employee.Id 
                && v.DateSince.Year == currentYear 
                && v.DateUntil < DateTime.Today
                && !v.IsPartialVacation)
                .Sum(v => (v.DateUntil - v.DateSince).Days + 1);

            return vacationPackage.GrantedDays - usedDays;
        }

        /* 
         * This method checks if an employee is eligible to request additional vacation days.
         It determines eligibility based on whether the employee has any free vacation days left for the current year.
         Parameters:
         - employee: The employee for whom to check vacation request eligibility.
         - vacations: A list of all vacations taken by employees.
         - vacationPackage: The vacation package assigned to the employee, which includes the total granted days.
         Returns:
         - A boolean value indicating whether the employee can request more vacation days (true if they have free days, false otherwise).
        */
        public bool IfEmployeeCanRequestVacation(Employee employee, List<Vacations> vacations, VacationPackage vacationPackage)
        {
            int freeDays = CountFreeDaysForEmployee(employee, vacations, vacationPackage);
            return freeDays > 0;
        }
    }
}
