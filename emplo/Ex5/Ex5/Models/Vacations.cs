namespace Ex5.Models
{
    public class Vacations
    {
        public int Id { get; set; }
        public DateTime DateSince { get; set; }
        public DateTime DateUntil { get; set; }
        public int NumberOfHours { get; set; }
        public bool IsPartialVacation { get; set; }
        public int EmployeeId { get; set; }
    }
}
