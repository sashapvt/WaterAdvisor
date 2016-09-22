using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WaterAdvisor.Models.Project
{
    public class ProjectBase
    {
        // General
        public int Id { get; set; }

        // Project info
        [Display(Name = "Назва проекту")]
        [StringLength(100)]
        public string ProjectName { get; set; }
        [Display(Name = "Коментар")]
        [StringLength(255)]
        public string ProjectComment { get; set; }
        [Display(Name = "Дата")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ProjectDate { get; set; }

        // Project correction data
        [Display(Name = "Рекавері")]
        public double RecoveryRO { get; set; } // RO recovery in % for concentrate parameters calculation
        [Display(Name = "Коригувати рН")]
        public EnumpHCorrection pHCorrection { get; set; } // 0 - none, 1 - HCl, 2 - H2SO4
        [Display(Name = "Скоригований рН")]
        public double pHCorrected { get; set; } // Desired pH

        // Enum for determine pHCorrection
        public enum EnumpHCorrection : int { None, HCl, H2SO4 }; // 0 - none, 1 - HCl, 2 - H2SO4

    }
}
