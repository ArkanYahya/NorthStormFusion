using System.ComponentModel.DataAnnotations;

namespace NorthStorm.Models.Assistants
{
    public class State
    {
        #region Model Properties
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } // Pending, Approved, Rejected, HasErrors  نماذج من القيم 
        #endregion

        #region Navigation Properties
        //public ICollection<Employee> Employees { get; set; }
        //public ICollection<Recruitment> Recruitments { get; set;}
        #endregion
    }
}
