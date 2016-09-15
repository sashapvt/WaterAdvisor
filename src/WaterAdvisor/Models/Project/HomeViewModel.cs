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

        // Project correction data
        public double RecoveryRO = 75; // RO recovery in % for concentrate parameters calculation
        public EnumpHCorrection pHCorrection = EnumpHCorrection.None; // 0 - none, 1 - HCl, 2 - H2SO4
        public double pHCorrected = 0; // Desired pH
        public double pHCorrectionAcidDose = 0; // Acid dose for pH correction, mg/l

        // Input water data
        public WaterList WaterIn { get; set; }

        // Calculation
        public Calc Calc { get; set; }

        // Enum for determine pHCorrection
        public enum EnumpHCorrection : int { None, HCl, H2SO4 }; // 0 - none, 1 - HCl, 2 - H2SO4
    }
}
