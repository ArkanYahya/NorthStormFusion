using System.ComponentModel.DataAnnotations;
using NorthStorm.Models.Assistants;
using NorthStorm.Models.Classifications;

namespace NorthStorm.Models.ViewModels
{

#warning try to use inheritance for shared properties
    public class LeaveCreateViewModel
    {
        [Required, Display(Name = "العدد")]
        public string ReferenceNo { get; set; }
        [Required, DataType(DataType.Date), Display(Name = "التاريخ")]
        public DateTime ReferenceDate { get; set; }= DateTime.Now;
        [Required, Display(Name = "الموضوع")]
        public string Subject { get; set; }

        [Required, Display(Name = "السبب")]
        public string LeaveReason { get; set; }



		public LeaveClassification LeaveClassification { get; set; }
		public List<int> EmployeeIds { get; set; }

		[Display(Name = "الاجازة")]
        public int? LeaveClassificationId { get; set; }

        //[Required, Display(Name = "مدة الاجازة/ يوم")] 
        [Required, Display(Name = "يوم")]
        public int LeaveInDays { get; set; }

        [Range(0, 11)]
        //  [Required, Display(Name = "مدة الاجازة/شهر")]
        [Required, Display(Name = "شهر")]
        public int LeaveInMonthes { get; set; }

        [Range(0, 5)]
        //[Required, Display(Name = "مدة الاجازة/سنة")]
        [Required, Display(Name = "سنة")]
        public int LeaveInYears { get; set; }

        [Display(Name = "ت الانفكاك")]
        public DateTime OnLeaveDate { get; set; } = DateTime.Now;


        [Display(Name = "المباشرة المفترض")]
        public DateTime SupposedEnrollDate { get; set; } = DateTime.Now;


        [Display(Name = "المباشرةالفعلي")]
        public DateTime EnrollDate { get; set; }


        //public LeaveClassification LeaveClassification { get; set; }
        //public List<int> EmployeeIds { get; set; }
    }

}
