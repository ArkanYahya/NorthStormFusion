using NorthStorm.Models.Assistants;
using NorthStorm.Models.Classifications;
using System.ComponentModel.DataAnnotations;

namespace NorthStorm.Models.Assistants
{
    public class Location
    {
        #region Model Properties
        [Display(Name = "المعرف")]
        public int Id { get; set; }
        [Display(Name = "الإسم")]
        public string Name { get; set; }
        #endregion

        #region Foreign Key
        [Display(Name = "التصنيف")]
        public int? ClassificationId { get; set; }
        [Display(Name = "اسم الموقع الجغرافي الأعلى")]
        public int? ParentLocationId { get; set; }
        #endregion

        #region Navigation Properties
        public Location ParentLocation { get; set; }
        public LocationClassification Classification { get; set; }
        public ICollection<Location> ChildLocations { get; set; }
        public ICollection<Level> Levels { get; set; }
        public ICollection<GovernmentalInstitute> GovernmentalInstitutes { get; set; }
        #endregion
    }
}