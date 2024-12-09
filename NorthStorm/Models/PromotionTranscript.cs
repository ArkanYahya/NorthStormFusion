using NorthStorm.Models.Assistants;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using NorthStorm.Models;

namespace NorthStorm.Models
{
    public class PromotionTranscript
    {
        #region Model Properties

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None), Display(Name = "رقم المحضر")]
        public int Id { get; set; }


        [StringLength(4, MinimumLength = 4)]
        [Required(ErrorMessage = "يرجى ادخال سنة المحضر"),
        Display(Name = "لسنة")]
        public int PromotionTranscriptYear { get; set; } = default(int);

        [Display(Name = " الموضوع")]
        public string Subject { get; set; }

        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        public int JobTitleId { get; set; }
        public JobTitle JobTitle { get; set; }




        [NotMapped, Display(Name = "عدد الموظفين")]
        public int EmployeeCount { get; set; }
        #endregion

        #region Navigation Properties
        public ICollection<Employee> Employees { get; set; } = new Collection<Employee>();
        #endregion
    }
}


