using System.ComponentModel.DataAnnotations;
using NorthStorm.Models.Assistants;
using NorthStorm.Models.Classifications;

namespace NorthStorm.Models.ViewModels
{
    public class RetirementEditViewModel
    {
        public int RetirementId { get; set; }

        [Required, Display(Name ="العدد")]
        public string ReferenceNo { get; set; }

        [Required, DataType(DataType.Date), Display(Name = "التاريخ")]
        public DateTime ReferenceDate { get; set; }

        [Required, Display(Name = "الموضوع")]
        public string Subject { get; set; } 
      
        [Display(Name = "إعتبارا من")]
        public DateTime DisengagementDate { get; set; }


        [Display(Name = "نوع التقاعد")]
        public int? DismissClassificationId { get; set; }
        public int? StatusId { get; set; }
        public DismissClassification DismissClassification { get; set; }
        public List<EmployeesInfo9> Employees { get; set; } = new List<EmployeesInfo9>();
    }

    public class EmployeesInfo9
    {
        public int EmployeeId { get; set; }
        public string FullName { get; set; }
    }
}
