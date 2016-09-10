using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WaterAdvisor.Models.Project
{
    public class Calc
    {
        public Calc(WaterList waterList)
        {
            this.w = waterList;
        }

        private WaterList w;
        private const double mol = 0.001;

        // CO2 concentration
        public double CO2 => _CO2(w.Temperature, w.HCO3, w.pH);
        // LSI index
        public double LSI => _LSI(w.TDS, w.Temperature, w.Ca, w.HCO3, w.pH);

        #region Calculation functions
        private double _CO2(WaterComponent temp, WaterComponent alk, WaterComponent pH)
        {
            double pK1 = 6.579 - 0.013 * temp.Value + 1.869e-4 * Math.Pow(temp.Value, 2) - 1.133e-6 * Math.Pow(temp.Value, 3) + 5.953e-9 * Math.Pow(temp.Value, 4); // corr of pK1 to temp 
            double lg_CO2 = Math.Log10(alk.ValueMEq * mol) - (pH.Value - pK1);//  Henderson–Hasselbalch equation 
            double CO2 = Math.Pow(10, lg_CO2) * 44 * 1000; // CO2 in ppm, CO2 in conc = CO2 in feed 
            return Math.Round(CO2, 2);
        }

        public static double _LSI(double tds, WaterComponent temp, WaterComponent calcium, WaterComponent alk, WaterComponent pH)
        {

            double tF = 9 * temp.Value / 5 + 32;

            double pCa = -Math.Log10((calcium.ValueMEq * 2 * mol));
            double pAlk = -Math.Log10((alk.ValueMEq * mol));
            //DuPont 1992, t here in F
            double C = 3.26 * Math.Exp(-0.005 * tF) - 0.0116 * Math.Log10(Math.Pow(tds, 3)) + 0.0905 * Math.Log10(Math.Pow(tds, 2)) - 0.133 * Math.Log10(tds) - 0.02;

            double LSI = pH.Value - (pCa + pAlk + C);
            return Math.Round(LSI, 2);
        }

        #endregion
    }
}
