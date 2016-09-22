using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace WaterAdvisor.Models.Project
{
    public class Project : ProjectBase
    {
        public Project()
        {
            RecoveryRO = 75;
            pHCorrection = (int)EnumpHCorrection.None;
            pHCorrected = 7;
        }
        // User
        [StringLength(450)]
        public string UserId { get; set; }

        // Input water quality
        public Water WaterIn { get; set; }
    }
}
