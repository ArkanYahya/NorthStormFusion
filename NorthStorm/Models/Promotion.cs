using NorthStorm.Models.Assistants;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace NorthStorm.Models
{
    public class Promotion
    {
        #region Model Properties
        public int Id { get; set; }

        [Required, Display(Name = "العدد")]
        public string ReferenceNo { get; set; }

        [Required, DataType(DataType.Date), Display(Name = "التاريخ")]
        public DateTime ReferenceDate { get; set; } = DateTime.Now.Date;

        [Required, Display(Name = " الموضوع / رقم الأمر الإداري")]
        public string Subject { get; set; }

        [NotMapped, Display(Name = "عدد الموظفين")]
        public int EmployeeCount { get; set; }
        #endregion

        #region Navigation Properties
        public ICollection<Employee> Employees { get; set; } = new Collection<Employee>();
        #endregion
    }
}
