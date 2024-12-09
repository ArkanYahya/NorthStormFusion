using System.ComponentModel.DataAnnotations;
using NorthStorm.Models.Assistants;
using NorthStorm.Models.Classifications;

namespace NorthStorm.Models.ViewModels
{
    public class PunishmentEditViewModel
    {
        public int PunishmentId { get; set; }

        [Required, Display(Name ="العدد")]
        public string ReferenceNo { get; set; }

        [Required, DataType(DataType.Date), Display(Name = "التاريخ")]
        public DateTime ReferenceDate { get; set; }

        [Required, Display(Name = "الموضوع")]
        public string Subject { get; set; } 
        
        [Required, Display(Name = "سبب العقوبة")]
        public string PunishmentReason { get; set; }

        [Display(Name = "العقوبة")]
        public int? PunishmentTypeId { get; set; }
        public PunishmentClassification PunishmentType { get; set; }
        public List<EmployeesInfo2> Employees { get; set; } = new List<EmployeesInfo2>();
    }

    public class EmployeesInfo2
    {
        public int EmployeeId { get; set; }
        public string FullName { get; set; }
    }
}
