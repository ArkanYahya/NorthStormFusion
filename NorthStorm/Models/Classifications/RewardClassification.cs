﻿using System.ComponentModel.DataAnnotations;

namespace NorthStorm.Models.Classifications

                    // انواع المكافأت و المساعدات المالية
{
    public class RewardClassification
    {
        #region Model Properties
        [Display(Name = "المعرف")]
        public int Id { get; set; }

        [Required, Display(Name = "التصنيف")]
        public string Name { get; set; }
        #endregion

        #region Navigation Properties
      //  public ICollection<Employee> Employees { get; set; }
        #endregion
    }
}
