using System.ComponentModel.DataAnnotations;
using NorthStorm.Models.Classifications;

namespace NorthStorm.Models.ViewModels
{

#warning try to use inheritance for shared properties
    public class JobPositionCreateViewModel
    {
        [Required, Display(Name = "العدد")]
        public string ReferenceNo { get; set; }
        [Required, DataType(DataType.Date), Display(Name = "التاريخ")]
        public DateTime ReferenceDate { get; set; }= DateTime.Now;
        [Required, Display(Name = "الموضوع")]
        public string Subject { get; set; }


        [Display(Name = "المنصب الوظيفي")]
        public int? ResponsibleClassificationId { get; set; }
        [Display(Name = "وكالة/ أصالة")]
        public int? DeputyClassificationId { get; set; }

        public ResponsibleClassification ResponsibleClassification { get; set; }
        public DeputyClassification DeputyClassification { get; set; }
        public List<int> EmployeeIds { get; set; }

       

        [Display(Name = "إعنبارا من")]
        public DateTime StartingFromDate { get; set; }


        //public JobPositionClassification JobPositionClassification { get; set; }
        //public List<int> EmployeeIds { get; set; }
    }

}
