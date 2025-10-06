namespace Ex3
{
    public class VacationsService
    {
        /* 
         * This method calculates the number of free vacation days an employee has left for the current year.
         * It takes into account the total granted days from the employee's vacation package and subtracts
         * the number of days already used in vacations that are not partial and have ended before today.
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
    }
}
