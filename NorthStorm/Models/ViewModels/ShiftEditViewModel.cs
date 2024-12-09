using System.ComponentModel.DataAnnotations;
using NorthStorm.Models.Assistants;
using NorthStorm.Models.Classifications;

namespace NorthStorm.Models.ViewModels
{
    public class ShiftEditViewModel
    {
        public int ShiftId { get; set; }

        [Required, Display(Name ="العدد")]
        public string ReferenceNo { get; set; }

        [Required, DataType(DataType.Date), Display(Name = "التاريخ")]
        public DateTime ReferenceDate { get; set; }

        [Required, Display(Name = "الموضوع")]
        public string Subject { get; set; } 
      
        [Display(Name = "إعتبارا من")]
        public DateTime EnrollDate { get; set; }


        [Display(Name = "الدوام/المناوبة")]
        public int? ShiftClassificationId { get; set; }
        public ShiftClassification ShiftClassification { get; set; }
        public List<EmployeesInfo6> Employees { get; set; } = new List<EmployeesInfo6>();
    }

    public class EmployeesInfo6
    {
        public int EmployeeId { get; set; }
        public string FullName { get; set; }
    }
}
