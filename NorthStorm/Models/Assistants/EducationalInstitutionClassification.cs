using System.ComponentModel.DataAnnotations;

namespace NorthStorm.Models.Assistants
{
    public class EducationalInstitutionClassification
    {
        #region Model Properties
        [Display(Name = "المعرف")]
        public int Id { get; set; }

        [Required, Display(Name = "التصنيف")]
        public string Name { get; set; }

        [Required, Display(Name = "الأهمية")]
        public int Rank { get; set; }
      
        #endregion

        #region Navigation Properties
        public ICollection<EducationalInstitution> EducationalInstitutions { get; set; }
        #endregion
    }
}
