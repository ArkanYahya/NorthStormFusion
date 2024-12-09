using System.ComponentModel.DataAnnotations;

namespace NorthStorm.Models.Classifications
{
    public class CreditedServiceClassification
    {
        #region Model Properties
        [Display(Name = "المعرف")]
        public int Id { get; set; }

        [Required, Display(Name = "التصنيف")]
        public string Name { get; set; }

        [Display(Name = "تحتسب للترقية")]
        public bool IsCountedForPromotion { get; set; }

        [Display(Name = "تحتسب للعلاوة")]
        public bool IsCountedForBouns { get; set; }
        [Display(Name = "تحتسب للتقاعد")]
        public bool IsCountedForRetirement { get; set; }

        #endregion

        #region

        #endregion


        #region Navigation Properties
        // public ICollection<Employee> Employees { get; set; }
        #endregion
    }
}
