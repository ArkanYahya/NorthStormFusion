using System.ComponentModel.DataAnnotations;
using NorthStorm.Models.Assistants;

namespace NorthStorm.Models.Classifications
{
    public class LocationClassification : Classification
    {
        #region Model Properties
        [Display(Name = "المعرف")]
        public int Id { get; set; }

        [Required, Display(Name = "التصنيف")]
        public string Name { get; set; }
        #endregion

        #region Navigation Properties
        public ICollection<Location> Locations { get; set; }
        #endregion
    }
}
