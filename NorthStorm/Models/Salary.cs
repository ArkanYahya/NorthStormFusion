namespace NorthStorm.Models
{
    public class Salary
    {
        public int Id { get; set; }

        public int BasicSalary { get; set; }

        public ICollection<Employee> Employees { get; set; }
    }
}
