using System.ComponentModel.DataAnnotations;
using NorthStorm.Models.Assistants;
using NorthStorm.Models.Classifications;

namespace NorthStorm.Models.ViewModels
{
    public class ThankAndAppreciationEditViewModel
    {
        public int ThankAndAppreciationId { get; set; }

        [Required, Display(Name = "العدد")]
        public string ReferenceNo { get; set; }

        [Required, DataType(DataType.Date), Display(Name = "التاريخ")]
        //  [DisplayFormat(DataFormatString = "{yyyy/MM/dd}")]
        public DateTime ReferenceDate { get; set; } = DateTime.Now;


        [Required, Display(Name = "الموضوع")]
        public string Subject { get; set; }


        [Required, Display(Name = "سبب الشكر")]
        public string ThankAndAppreciationReason { get; set; }


        [Display(Name = "القدم الممنوح/ شهر")]
        public int ThankSeniority { get; set; }
        //public List<int> EmployeeIds { get; set; }

        [Display(Name = "التقييم")]
        public int? ThankClassificationId { get; set; }
        public ThankClassification ThankClassification { get; set; }
        public List<EmployeesInfo5> Employees { get; set; } = new List<EmployeesInfo5>();
    }

    public class EmployeesInfo5
    {
        public int EmployeeId { get; set; }
        public string FullName { get; set; }
    }
}
