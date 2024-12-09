using System.ComponentModel.DataAnnotations;
using NorthStorm.Models.Assistants;
using NorthStorm.Models.Classifications;

namespace NorthStorm.Models.ViewModels
{

#warning try to use inheritance for shared properties
    public class PromotionCreateViewModel
    {
        [Display(Name = "الوجبة")]
        public string BatchNo { get; set; }

        [Required, Display(Name = "السنة")]
        public int PromotionMinutesYear { get; set; }
        [ Display(Name = "الموضوع")]
        public string Subject { get; set; }

        [Display(Name = "رقم الامر")]
        public string OrderNo { get; set; }

        [Display(Name = "العدد")]
        public string ReferenceNo { get; set; }

        [DataType(DataType.Date), Display(Name = "التاريخ")]
        public DateTime ReferenceDate { get; set; } = DateTime.Now;



        //public PromotionClassification PromotionClassification { get; set; }
        public List<int> EmployeeIds { get; set; }
    }

}
