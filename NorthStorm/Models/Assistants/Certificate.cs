﻿using Microsoft.VisualBasic;
using NorthStorm.Models.Assistants;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace NorthStorm.Models.Assistants
{
    public class Certificate          // الشهادات الدراسية
    {
        #region Model Properties
        public int Id { get; set; }

        [StringLength(25, MinimumLength = 2)]
        [Required(ErrorMessage = "يرجى ادخال اسم الشهادة الدراسية")]
        [Display(Name = "الشهادة الدراسية")]
        public string Name { get; set; }

        [StringLength(30, MinimumLength = 2)]
        [Required(ErrorMessage = "يرجى ادخال اسم الشهادة الدراسية المؤثرة في حسابات النظام مثل بكالوريوس 5 سنوات")]
        [Display(Name = "الشهادة المؤثرة")]
        public string InflueritialCertificate { get; set; }


        [Range(5, 10)]
        [Required(ErrorMessage = "يرجى ادخال الدرجة رقما التي يتعين عليها الحاصل على الشهادة")]
        [Display(Name = "التعيين في الدرجة")]
        public int GradeStart { get; set; }

        [Range(1, 6)]
        [Required(ErrorMessage = "يرجى ادخال المرحلة رقما التي يتعين عليها الحاصل على الشهادة")]
        [Display(Name = "التعيين في المرحلة")]
        public int StageStart { get; set; }

        [Range(1, 5)]
        [Required(ErrorMessage = "يرجى ادخال اخر درجة رقما التي يصل اليها الحاصل على الشهادة")]
        [Display(Name = "درجة التوقف")]
        public int GradeEnd { get; set; }


        [Range(0, 100)]
        [Required(ErrorMessage = "يرجى ادخال النسبة المئوية لمخصصات الشهادة"),
        Display(Name = "نسبة المخصصات")]
        public int AllocationPercentage { get; set; }

        [Range(0, 100)]
        [Required(ErrorMessage = "يرجى ادخال المدة الاصغرية لأول ترقية فقط"),
        Display(Name = "مدة أول ترقية")]
        public int FirstPromotionDuration { get; set; }



        [Display(Name = "الرمز القديم")]
        public string CertificateOldCode { get; set; }

        #endregion

        #region Navigstion Properties

        public ICollection<Employee> Employees { get; set; } = new Collection<Employee>();
        // public ICollection<GovernmentalInstitute> GovernmentalInstitutes { get; set; } = new Collection<GovernmentalInstitute>();
        #endregion


        #region Not Mapped Properties
        [NotMapped, Display(Name = "عدد الموظفين")]
        public int EmployeeCount { get; set; }
        #endregion



    }
}


