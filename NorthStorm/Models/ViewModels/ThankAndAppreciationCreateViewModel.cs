using System.ComponentModel.DataAnnotations;
using NorthStorm.Models.Assistants;
using NorthStorm.Models.Classifications;

namespace NorthStorm.Models.ViewModels
{

#warning try to use inheritance for shared properties

    public class ThankAndAppreciationCreateViewModel
    {
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

        //[Required(ErrorMessage = "يرجى ادخال الدرجة الوظيفية"),
        //Display(Name = "درجة العنوان الوظيفي")]
        //public int JobGradeNumber { get; set; }



        [Display(Name = "المخول بالشكر")]
        public int? ThankClassificationId { get; set; }
        public ThankClassification ThankClassification { get; set; }
        public List<int> EmployeeIds { get; set; }

        //public int? LeaveClassificationId { get; set; }


    }

}
