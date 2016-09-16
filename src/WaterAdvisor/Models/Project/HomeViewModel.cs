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
            //RecoveryRO = 75;
            //pHCorrection = (int) EnumpHCorrection.None;
            //pHCorrected = 0;
            //pHCorrectionAcidDose = 0;
            WaterIn = new WaterList();
            Calc = new Calc(WaterIn);
        }

        // Input water data
        public WaterList WaterIn { get; set; }

        // Calculation
        public Calc Calc { get; set; }
    }
}
