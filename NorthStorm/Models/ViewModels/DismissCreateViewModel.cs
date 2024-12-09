using System.ComponentModel.DataAnnotations;
using NorthStorm.Models.Assistants;

namespace NorthStorm.Models.ViewModels
{

#warning try to use inheritance for shared properties
    public class DismissCreateViewModel
    {
        [Required, Display(Name = "العدد")]
        public string ReferenceNo { get; set; }

        [Required, Display(Name = "رقم الامر")]
        public string OrderNo { get; set; }

        [Required, DataType(DataType.Date), Display(Name = "تاريخه")]
        public DateTime OrderDate { get; set; }= DateTime.Now;

        [Required, DataType(DataType.Date), Display(Name = "إعتبارا من")]
        public DateTime BreakUpDate { get; set; } = DateTime.Now;
              

        [Required, Display(Name = "نوع مغادرة الملاك")]
        public int DismissTypeId { get; set; }


        public List<int> TheEmployeeIds { get; set; }
    }

}
