using Microsoft.EntityFrameworkCore;

namespace LogisticCalculationMVC.Models
{
    public class EmployeeRepository
    {
        private EmployeeDBContext dbContext { get; }

        public EmployeeRepository()
        {
            var optionBuilder = new DbContextOptionsBuilder<EmployeeDBContext>();
            optionBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=EmployeeDB;Integrated Security=True");

            dbContext = new EmployeeDBContext(optionBuilder.Options);
        }

        // Add methods to fetch employees and workplaces from the dbContext
        public List<EmployeeViewModel> GetEmployees()
        {
            return dbContext.Employees
        .Include(e => e.WorkplaceNavigation)
        .Select(e => new EmployeeViewModel
        {
            Id = e.Id,
            Name = e.Name,
            Surname = e.Surname,
            Birth = e.Birth,
            Job = e.Job,
            WorkplaceName = e.WorkplaceNavigation.WorkplaceName
        })
        .ToList();
        }

        public List<WorkplaceViewModel> GetWorkplaces()
        {
            return dbContext.Workplaces
            .Include(e => e.Employees)
            .Select(e => new WorkplaceViewModel
            {
                Id = e.Id,
                WorkplaceName = e.WorkplaceName,
                City = e.City,
                Street = e.Street,
                Psc = e.Psc,
                Email = e.Email,
                Phone = e.Phone,
                EmployeeCount = e.Employees.Count
            }).ToList();
        }
    }
}