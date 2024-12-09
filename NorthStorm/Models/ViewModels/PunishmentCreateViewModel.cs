using System.ComponentModel.DataAnnotations;
using NorthStorm.Models.Assistants;
using NorthStorm.Models.Classifications;

namespace NorthStorm.Models.ViewModels
{

#warning try to use inheritance for shared properties
    public class PunishmentCreateViewModel
    {
        [Required, Display(Name = "العدد")]
        public string ReferenceNo { get; set; }
        [Required, DataType(DataType.Date), Display(Name = "التاريخ")]
        public DateTime ReferenceDate { get; set; }= DateTime.Now;
        [Required, Display(Name = "الموضوع")]
        public string Subject { get; set; }

        [Required, Display(Name = "سبب العقوبة")]
        public string PunishmentReason { get; set; }

        [Display(Name = "العقوبة")]
        public int? PunishmentTypeId { get; set; }
        
        public PunishmentClassification PunishmentType { get; set; }
        public List<int> EmployeeIds { get; set; }
    }

}
