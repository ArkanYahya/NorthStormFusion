using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace NorthStorm.Models.Classifications
{
    public class PrivilegeClassification           // تصنيفات الامتياز الوظيفي
    {
        #region Model Properties

        [Display(Name = "المعرف")]
        public int Id { get; set; }

        [Required(ErrorMessage = "ادخال نوع الامتياز مثل الفصل السياسي او ذوي الشهداء ... الخ"),]
        [Display(Name = "الامتياز الوظيفي")]
        public string Name { get; set; }

        [Range(0, 10)]
        [Required(ErrorMessage = "يرجى إدخال عدد سنوات التمديد"),]
        [Display(Name = "نمديد سن التقاعد/سنة")]
        public int ExtendingRetirementAge  { get; set; }
       
        [Required(ErrorMessage = "يرجى إدخال نعم في حال وجود امتياز"),]
        [Display(Name = "امتياز للترقية")]
        public bool PromotionBenefites { get; set; }
        
        [Display(Name = "امتيازات اخرى")]
        public bool OtherBenefites { get; set; } 

        #endregion

        #region Navigstion Properties
      //  public ICollection<Employee> Employees { get; set; }
        #endregion
    }
}
