using Microsoft.CodeAnalysis;
using NorthStorm.Models.Classifications;
using System.ComponentModel.DataAnnotations;

namespace NorthStorm.Models.Assistants
{
    public class GovernmentalInstitute
    {
        #region Model Properties
        [Display(Name = "المعرف")]
        public int Id { get; set; }
        [Required, Display(Name = "اسم المؤسسة")]
        public string Name { get; set; }
        #endregion

        #region Foreign Key
        [Display(Name = "التصنيف")]
        public int? ClassificationId { get; set; }
        [Display(Name = "المؤسسة الأعلى")]
        public int? ParentGovernmentalInstituteId { get; set; }
        [Display(Name = "الموقع الجغرافي")]
        public int? LocationId { get; set; }
        #endregion

        #region Navigation Properties
        public GovernmentalInstituteClassification Classification { get; set; }
        public GovernmentalInstitute ParentGovernmentalInstitute { get; set; }
        public Location Location { get; set; }
        public ICollection<GovernmentalInstitute> ChildGovernmentalInstitutes { get; set; }
        #endregion
    }
}