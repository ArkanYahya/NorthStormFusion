using Microsoft.VisualBasic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NorthStorm.Models.Assistants
{
    public class Certificate
    {
        #region Model Properties
        public int Id { get; set; }

        [Required, Display(Name = "الدرجة")]
        public string Degree { get; set; }

        [Required, Display(Name = "التخصص العام")]
        public string GlobalSpecialization { get; set; }

        [Required, Display(Name = "التخصص الدقيق")]
        public string AccurateSpecialization { get; set; }

        [Range(1900, 2100)]
        [Required, Display(Name = "سنة التخرج")]
        public int Year { get; set; }
        #endregion

        #region Foreign Key
        [Required, Display(Name = "الجامعة أو المؤسسة")]
        public int? UniversityId { get; set; }
        #endregion

        #region Navigation Properties
        public GovernmentalInstitute University { get; set; }
        public ICollection<Employee> Employees { get; set; } = new Collection<Employee>();
        #endregion

        #region Not Mapped Properties
        [NotMapped, Display(Name = "عدد الموظفين")]
        public int EmployeeCount { get; set; }
        #endregion
    }
}
