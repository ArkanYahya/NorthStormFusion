using System.ComponentModel.DataAnnotations;
using NorthStorm.Models.Assistants;

namespace NorthStorm.Models.ViewModels
{
    public class JobTransferEditViewModel
    {
        public int JobTransferId { get; set; }
        [Required, Display(Name ="العدد")]
        public string ReferenceNo { get; set; }
        [Required, DataType(DataType.Date), Display(Name = "التاريخ")]
        public DateTime ReferenceDate { get; set; }
        [Required, Display(Name = "الموضوع")]
        public string Subject { get; set; }

        [Display(Name = "نقل من")]
        public int? CurrentLevelId { get; set; }

        [Display(Name = "نقل إلى")]
        public int? DestinationLevelId { get; set; }
        public Level CurrentLevel { get; set; }
        public Level DestinationLevel { get; set; }
        public List<EmployeesInfo> Employees { get; set; } = new List<EmployeesInfo>();
    }

    public class EmployeesInfo
    {
        public int EmployeeId { get; set; }
        public string FullName { get; set; }
    }
}
