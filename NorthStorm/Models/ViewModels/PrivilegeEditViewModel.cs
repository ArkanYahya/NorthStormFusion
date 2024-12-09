using System.ComponentModel.DataAnnotations;
using NorthStorm.Models.Assistants;
using NorthStorm.Models.Classifications;

namespace NorthStorm.Models.ViewModels
{
    public class PrivilegeEditViewModel
    {
        public int PrivilegeId { get; set; }

        [Required, Display(Name ="العدد")]
        public string ReferenceNo { get; set; }

        [Required, DataType(DataType.Date), Display(Name = "التاريخ")]
        public DateTime ReferenceDate { get; set; }

        [Required, Display(Name = "الموضوع")]
        public string Subject { get; set; } 
      
       
        [Display(Name = "الإمتياز الوظيفي")]
        public int? PrivilegeClassificationId { get; set; }
        public PrivilegeClassification PrivilegeClassification { get; set; }
        public List<EmployeesInfo8> Employees { get; set; } = new List<EmployeesInfo8>();
    }

    public class EmployeesInfo8
    {
        public int EmployeeId { get; set; }
        public string FullName { get; set; }
    }
}
