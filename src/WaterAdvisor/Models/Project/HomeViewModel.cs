using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WaterAdvisor.Models.Project
{
    public class HomeViewModel : ProjectBase
    {
        // Input water data
        public WaterList WaterIn { get; set; }
    }
}
