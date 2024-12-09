using System.ComponentModel.DataAnnotations;

namespace NorthStorm.Models.Classifications
{
    public class CollegeName          // قائمة الكليات و المعاهد
    {
        #region Model Properties

        [Display(Name = "المعرف")]
        public int Id { get; set; }

        [Required]
        [Display(Name = "الكلية/ المعهد")]
        public string Name { get; set; }


        [Display(Name = "الرمز القديم")]
        public string CollegeOldCode { get; set; }

        [Display(Name = "رمز SPSS للكليات")]
        public int CollegeSPSS1 { get;set; }



        #endregion

        #region Navigstion Properties
        //public ICollection<Employee> Employees { get; set; }
        #endregion
    }
}
