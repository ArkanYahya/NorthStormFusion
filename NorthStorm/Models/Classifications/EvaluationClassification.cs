using System.ComponentModel.DataAnnotations;

namespace NorthStorm.Models.Classifications
{
    public class EvaluationClassification
    {
        #region Model Properties
        [Display(Name = "المعرف")]
        public int Id { get; set; }

        [Required, Display(Name = "التصنيف")]
        public string Name { get; set; }

        [Range (0,100)]
        [Required, Display(Name = "الحد الادنى")]
        public int LowEvaluationLimit { get; set; }

        [Range(0, 100)]
        [Required, Display(Name = "الحد الاعلى")]
        public int UpperEvaluationLimit { get; set; }

        [Required, Display(Name = "تأثير الترقية")]
        public bool PromotionEffect { get; set; }

        [Required, Display(Name = "تأثير الحوافز")]
        public bool MotivationEffect { get; set; }

        [Required, Display(Name = "تأثير العلاوة")]
        public bool BonusEffect { get; set; }

        #endregion

        #region Navigation Properties
     //   public ICollection<Employee> Employees { get; set; }
        #endregion
    }
}
