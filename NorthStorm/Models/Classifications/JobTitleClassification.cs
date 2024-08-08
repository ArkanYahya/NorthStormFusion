using System.ComponentModel.DataAnnotations;

namespace NorthStorm.Models.Classifications
{
    public class JobTitleClassification : Classification
    {
        #region Model Properties
        [Display(Name = "المعرف")]
        public int Id { get; set; }

        [Required, Display(Name = "التصنيف")]
        public string Name { get; set; }
        #endregion

        #region Navigation Properties
        public ICollection<JobTitle> JobTitles { get; set; }
        #endregion
    }
}
