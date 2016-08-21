using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WaterAdvisor.Models.Project
{
    public class Project
    {
        // General
        public int Id { get; set; }
        [StringLength(450)]
        public string UserId { get; set; }

        // Інформація про проект
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
    }
}
