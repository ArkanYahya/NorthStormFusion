using System.ComponentModel.DataAnnotations;
using NorthStorm.Models.Classifications;

namespace NorthStorm.Models.ViewModels
{
    public class JobPositionEditViewModel
    {
        public int JobPositionId { get; set; }

        [Required, Display(Name ="العدد")]
        public string ReferenceNo { get; set; }

        [Required, DataType(DataType.Date), Display(Name = "التاريخ")]
        public DateTime ReferenceDate { get; set; }

        [Required, Display(Name = "الموضوع")]
        public string Subject { get; set; } 
      
        [Display(Name = "إعتبارا من")]
        public DateTime StartingFromDate { get; set; }


        [Display(Name = "المنصب الوظيفي")]
        public int? ResponsibleClassificationId { get; set; }

        [Display(Name = "وكالة/ أصالة")]
        public int? DeputyClassificationId { get; set; }
       
        public ResponsibleClassification ResponsibleClassification { get; set; }
        public DeputyClassification DeputyClassification { get; set; }
        public List<EmployeesInfo10> Employees { get; set; } = new List<EmployeesInfo10>();
    }

    public class EmployeesInfo10
    {
        public int EmployeeId { get; set; }
        public string FullName { get; set; }
    }
}
