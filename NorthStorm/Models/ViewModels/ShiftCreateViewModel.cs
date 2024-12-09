using System.ComponentModel.DataAnnotations;
using NorthStorm.Models.Assistants;
using NorthStorm.Models.Classifications;

namespace NorthStorm.Models.ViewModels
{

#warning try to use inheritance for shared properties
    public class ShiftCreateViewModel
    {
        [Required, Display(Name = "العدد")]
        public string ReferenceNo { get; set; }
        [Required, DataType(DataType.Date), Display(Name = "التاريخ")]
        public DateTime ReferenceDate { get; set; }= DateTime.Now;
        [Required, Display(Name = "الموضوع")]
        public string Subject { get; set; }

		public ShiftClassification ShiftClassification { get; set; }
		public List<int> EmployeeIds { get; set; }

		[Display(Name = "المناوبة")]
        public int? ShiftClassificationId { get; set; }

        [Display(Name = "إعنبارا من")]
        public DateTime EnrollDate { get; set; }


        //public ShiftClassification ShiftClassification { get; set; }
        //public List<int> EmployeeIds { get; set; }
    }

}
