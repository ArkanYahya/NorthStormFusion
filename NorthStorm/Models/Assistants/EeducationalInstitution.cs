using Microsoft.CodeAnalysis;
using NorthStorm.Models.Classifications;
using System.ComponentModel.DataAnnotations;

namespace NorthStorm.Models.Assistants
{
    public class EducationalInstitution         // المؤسسات التعليمية
    {
        #region Model Properties
        [Display(Name = "المعرف")]
        public int Id { get; set; }
        [Required, Display(Name = "المؤسسة التعليمية")]
        public string Name { get; set; }
        #endregion

        #region Foreign Key
        [Display(Name = "اسم المؤسسة الأعلى")]
        public int? ParentEducationalInstitutionId { get; set; }
        [Display(Name = "الموقع الجغرافي")]
        public int? LocationId { get; set; }
        [Display(Name = "التصنيف")]
        public int? ClassificationId { get; set; }

        #endregion

        #region Navigation Properties
        public EducationalInstitution ParentEducationalInstitution { get; set; }
        public Location Location { get; set; }
        public EducationalInstitutionClassification Classification { get; set; }
        public ICollection<EducationalInstitution> ChildEducationalInstitutions { get; set; }
        #endregion
    }
}