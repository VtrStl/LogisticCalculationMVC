namespace LogisticCalculationMVC.Models
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public DateTime Birth { get; set; }
        public string? Job { get; set; }
        public string? WorkplaceName { get; set; }
        public virtual Workplace? WorkplaceNavigation { get; set; }
    }

    public class WorkplaceViewModel
    {
        public int Id { get; set; }
        public string? WorkplaceName { get; set; }
        public string? City { get; set; }
        public string? Street { get; set; }
        public int Psc { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public int EmployeeCount { get; set; }

        public virtual ICollection<Employee>? Employees { get; set; }
    }
}