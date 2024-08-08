using System.ComponentModel.DataAnnotations;
using NorthStorm.Models.Assistants;

namespace NorthStorm.Models.ViewModels
{

#warning try to use inheritance for shared properties
    public class JobTransferCreateViewModel
    {
        [Required, Display(Name = "العدد")]
        public string ReferenceNo { get; set; }
        [Required, DataType(DataType.Date), Display(Name = "التاريخ")]
        public DateTime ReferenceDate { get; set; }= DateTime.Now;
        [Required, Display(Name = "الموضوع")]
        public string Subject { get; set; }
        public int? DestinationLevelId { get; set; }
        [Display(Name = "نقل إلى")]
        public Level DestinationLevel { get; set; }
        public List<int> EmployeeIds { get; set; }
    }

}
