﻿using System.ComponentModel.DataAnnotations;

namespace NorthStorm.Models
{
    public class TmpBonus
    {
        #region Model Properties
        [Display(Name = "المعرف")]
        public int Id { get; set; }

        [Required, Display(Name = "رقم الموظف")]
        public int EmployeeId { get; set; }

        [Display(Name = "تاريخ الترقية القادمة")]
        public DateTime? NextDate  { get; set; }
        #endregion

        #region Foreign Key
        // No singular navigation property
        #endregion

        #region Navigation Properties
        public ICollection<Employee> Employees { get; set; }
        #endregion  
    }
}