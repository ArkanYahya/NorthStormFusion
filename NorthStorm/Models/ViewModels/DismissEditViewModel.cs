using System.ComponentModel.DataAnnotations;
using NorthStorm.Models.Assistants;

namespace NorthStorm.Models.ViewModels
{
    public class DismissEditViewModel
    {
        public int DismissId { get; set; }
        [Required, Display(Name ="العدد")]
        public string ReferenceNo { get; set; }

        [Required, Display(Name = "رقم الامر")]
        public string OrderNo { get; set; }

        [Required, DataType(DataType.Date), Display(Name = "تاريخه")]
        public DateTime OrderDate { get; set; } = DateTime.Now;

        [Required, DataType(DataType.Date), Display(Name = "إعتبارا من")]
        public DateTime BreakUpDate { get; set; } = DateTime.Now;

        [Required, Display(Name = "نوع مغادرة الملاك")]
        public int? DismissTypeId { get; set; }
       
        public List<TheEmployeesInfo> Employees { get; set; } = new List<TheEmployeesInfo>();
    }


    public class TheEmployeesInfo
    {
        public int TheEmployeeId { get; set; }
        public string FullName { get; set; }
    }
}
