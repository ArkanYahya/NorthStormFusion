using Microsoft.VisualBasic;
using NorthStorm.Models.Assistants;
using NorthStorm.Models.Classifications;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NorthStorm.Models
{
    public class Privilege
    {
        #region Model Properties
        public int Id { get; set; }

        [Required, Display(Name = "العدد")]
        public string ReferenceNo { get; set; }

        [Required, DataType(DataType.Date), Display(Name = "التاريخ")]
        public DateTime ReferenceDate { get; set; } = DateTime.Now;

        [Required, Display(Name = "الموضوع")]
        public string Subject { get; set; }
     
       #endregion

        #region Foreign Key
        [Display(Name = "الامتياز الوظيفي")]
        public int? PrivilegeClassificationId { get; set; }
        #endregion

        #region Navigation Properties
        public PrivilegeClassification PrivilegeClassification { get; set; }
        public ICollection<Employee> Employees { get; set; } = new Collection<Employee>();
        #endregion

        #region Not Mapped Properties
        [NotMapped, Display(Name = "عدد الموظفين")]
        public int EmployeeCount { get; set; }
        #endregion
    }
}
