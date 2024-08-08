using System.ComponentModel.DataAnnotations;
using NorthStorm.Models.Assistants;
using NorthStorm.Models.Classifications;

namespace NorthStorm.Models
{
    public class JobTitle
    {
        #region Model Properties
        [Display(Name = "المعرف")]
        public int Id { get; set; }
        
        [Required, Display(Name = "العنوان الوظيفي")]
        public string Name { get; set; }

        [Display(Name = "الوصف الوظيفي")]
        public string Description { get; set; }
        #endregion

        #region Foreign Key
        [Display(Name = "العنوان الأعلى")]
        public int? ParentJobTitleId { get; set; }
        [Display(Name = "التصنيف")]
        public int? ClassificationId { get; set; }
        [Range(1, 10)]
        [Display(Name = "الدرجة")]
        public int? GradeId { get; set; }
        #endregion

        #region Navigation Properties
        public JobTitle ParentJobTitle { get; set; }
        public ICollection<JobTitle> ChildJobTitles { get; set; }
        public JobTitleClassification Classification { get; set; }
        public Grade Grade { get; set; }
        public ICollection<Level> Levels { get; set; }  
        public ICollection<EmployeeJobTitle> EmployeeJobTitles { get; set; }
        #endregion
    }
}

