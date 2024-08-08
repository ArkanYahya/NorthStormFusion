using NorthStorm.Models.Assistants;
using System.ComponentModel.DataAnnotations;

namespace NorthStorm.Models.Classifications
{
    public class LevelClassification : Classification
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
        public ICollection<Level> Levels { get; set; }
        #endregion
    }
}
