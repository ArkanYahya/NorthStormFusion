using Microsoft.CodeAnalysis;
using NorthStorm.Models.Classifications;
using System.ComponentModel.DataAnnotations;

namespace NorthStorm.Models.Assistants
{
    public class Level
    {
        #region Model Properties
        [Display(Name = "المعرف")]
        public int Id { get; set; }
        [Display(Name = "اسم الوحدة الادارية")]
        public string Name { get; set; }
        #endregion


        #region Foreign Key
        [Display(Name = "اسم الوحدة الإدارية الأعلى")]
        public int? ParentLevelId { get; set; }
        [Display(Name = "الموقع الجغرافي")]
        public int? LocationId { get; set; }
        [Display(Name = "التصنيف")]
        public int? ClassificationId { get; set; }
        #endregion


        #region Navigation Properties
        public Level ParentLevel { get; set; }
        public Location Location { get; set; }
        public LevelClassification Classification { get; set; }
        public ICollection<JobTransfer> JobTransfers { get; set; }
        public ICollection<JobTitle> JobTitles { get; set; }
        public ICollection<Level> ChildLevels { get; set; }
        public ICollection<Employee> Employees { get; set; }
        #endregion
    }
}