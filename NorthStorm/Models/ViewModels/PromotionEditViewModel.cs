using System.ComponentModel.DataAnnotations;
using NorthStorm.Models.Assistants;
using NorthStorm.Models.Classifications;

namespace NorthStorm.Models.ViewModels
{
    public class PromotionEditViewModel
    {
        public int PromotionId { get; set; }

        [Display(Name = "الوجبة")]
        public string BatchNo { get; set; }

        [Required, Display(Name = "السنة")]
        public int PromotionMinutesYear { get; set; }

        [Display(Name = "الموضوع")]
        public string Subject { get; set; }

        [Display(Name = "رقم الامر")]
        public string OrderNo { get; set; }

        [Display(Name = "العدد")]
        public string ReferenceNo { get; set; }

        [DataType(DataType.Date), Display(Name = "التاريخ")]
        public DateTime ReferenceDate { get; set; } = DateTime.Now;

        public List<EmployeesInfo14> Employees { get; set; } = new List<EmployeesInfo14>();
    }

    public class EmployeesInfo14
    {
        public int EmployeeId { get; set; }
        public string FullName { get; set; }
    }
}
