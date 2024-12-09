using System.ComponentModel.DataAnnotations;

namespace NorthStorm.Models.Classifications
{
    public class OnLeaveAndEnroll
    {
        [Display(Name ="تاريخ الانفكاك")]
        public DateTime OnLeaveDate { get; set; }

        [Display(Name ="تاريخ المباشرة")]
        public DateTime EnrollDate { get; set; }
    }
}
