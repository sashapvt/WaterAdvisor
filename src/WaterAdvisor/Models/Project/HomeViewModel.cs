using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WaterAdvisor.Models.Project
{
    public class HomeViewModel : ProjectBase
    {
        public HomeViewModel()
        {
            WaterIn = new WaterList();
            Calc = new Calc(WaterIn);
        }
        // Input water data
        public WaterList WaterIn { get; set; }

        // Calculation
        public Calc Calc { get; set; }
    }
}
