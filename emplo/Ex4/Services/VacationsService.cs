using Ex4.Models;

namespace Ex4.Services
{
    public class VacationsService
    {
        /* 
         * This method calculates the number of free vacation days an employee has left for the current year.
         It takes into account the vacations already taken by the employee and the granted vacation days from their vacation package.
         Parameters:
         - employees: The employee for whom to calculate free vacation days.
         - vacations: A list of all vacations taken by employees.
         - vacationPackage: The vacation package associated with the employee, which includes granted days.
         Returns:
         - An integer representing the number of free vacation days remaining for the employee in the current year.
        */
        public int CountFreeDaysForEmployee(Employee employees,List<Vacations> vacations,VacationPackage vacationPackage)
        {
            var currentYear = DateTime.Now.Year;

            var usedDays = vacations
                .Where(v => v.EmployeeId == employees.Id 
                && v.DateSince.Year == currentYear 
                && v.DateUntil < DateTime.Today
                && !v.IsPartialVacation)
                .Sum(v => (v.DateUntil - v.DateSince).Days + 1);

            return vacationPackage.GrantedDays - usedDays;
        }

        /* 
         * This method checks if an employee is eligible to request additional vacation days based on their remaining free days.
         It utilizes the CountFreeDaysForEmployee method to determine if the employee has any free days left.
         Parameters:
         - employee: The employee for whom to check vacation request eligibility.
         - vacations: A list of all vacations taken by employees.
         - vacationPackage: The vacation package associated with the employee, which includes granted days.
         Returns:
         - A boolean value indicating whether the employee can request more vacation days (true) or not (false).
        */
        public bool IfEmployeeCanRequestVacation(Employee employee, List<Vacations> vacations, VacationPackage vacationPackage)
        {
            int freeDays = CountFreeDaysForEmployee(employee, vacations, vacationPackage);
            return freeDays > 0;
        }
    }
}
