using System.ComponentModel.DataAnnotations;
using NorthStorm.Models.Assistants;
using NorthStorm.Models.Classifications;

namespace NorthStorm.Models.ViewModels
{

#warning try to use inheritance for shared properties
    public class RetirementCreateViewModel
    {
        [Required, Display(Name = "العدد")]
        public string ReferenceNo { get; set; }
        [Required, DataType(DataType.Date), Display(Name = "التاريخ")]
        public DateTime ReferenceDate { get; set; }= DateTime.Now;
        [Required, Display(Name = "الموضوع")]
        public string Subject { get; set; }

		public DismissClassification DismissClassification { get; set; }
		public List<int> EmployeeIds { get; set; }

		[Display(Name = "نوع التقاعد")]
        public int? DismissClassificationId { get; set; }
        public int? StatusId { get; set; }

        [Display(Name = "إعنبارا من")]
        public DateTime DisengagementDate { get; set; }


        //public RetirementClassification RetirementClassification { get; set; }
        //public List<int> EmployeeIds { get; set; }
    }

}
