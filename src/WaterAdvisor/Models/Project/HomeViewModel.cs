using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WaterLibrary;

namespace WaterAdvisor.Models.Project
{
    public class HomeViewModel
    {
        public HomeViewModel()
        {
            P = new ProjectBase();
            WaterIn = new WaterList();
            Calc = new Calc(P, WaterIn);
        }

        // Project Id
        public int Id => P.Id;

        // Current project
        public ProjectBase P { get; set; }

        // Input water data
        public WaterList WaterIn { get; set; }

        // Calculation
        public Calc Calc { get; set; }
    }
}
