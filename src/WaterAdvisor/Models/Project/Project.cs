using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WaterAdvisor.Models.Project
{
    public class Project : ProjectBase
    {
        // User
        [StringLength(450)]
        public string UserId { get; set; }

        // Input water quality
        public Water WaterIn { get; set; }
    }
}
