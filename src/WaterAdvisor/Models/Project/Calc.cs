using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WaterAdvisor.Models.Project
{
    public class Calc
    {
        public Calc(ProjectBase p, WaterList waterList)
        {
            this.p = p;
            this.w = waterList;
        }

        private ProjectBase p;
        private WaterList w;
        private const double mol = 0.001;

        // IS (Ionic streight)
        public double IS => _IS(w);
        // CO2 concentration
        public double CO2 => _CO2(w.Temperature, w.HCO3, w.pH.Value);
        // LSI index
        public double LSI => _LSI(w.TDS, w.Temperature, w.Ca, w.HCO3, w.pH);
        // S&DSI index
        public double StDI => _StDI(IS, w.Temperature, w.Ca, w.HCO3, w.pH);
        // IP CaSO4
        public double IP_CaSO4 => _IP_CaSO4(w.Ca, w.SO4, IS, w.Temperature);
        // IP BaSO4
        public double IP_BaSO4 => _IP_BaSO4(w.Ba, w.SO4, IS);
        // IP SrSO4
        public double IP_SrSO4 => _IP_SrSO4(w.Sr, w.SO4, IS);
        // Acid dose for pH correction, mg/l
        public double pHCorrectionAcidDose => _pHCorrectionAcidDose(p.pHCorrection, p.pHCorrected, w.HCO3, w.pH, w.Temperature, _CO2(w.Temperature, w.HCO3, p.pHCorrected));

        #region Calculation functions
        // IS (Ionic streight)
        private double _IS(WaterList wL)
        {
            return Math.Round(0.5 * (wL.Cations().Sum(x => x.ValueMEq * Math.Pow(x.GetIonCharge(), 3)) + wL.Anions().Sum(x => x.ValueMEq * Math.Pow(x.GetIonCharge(), 3))) * mol, 3);
        }

        // CO2 concentration
        private double _CO2(WaterComponent temp, WaterComponent alk, double pH)
        {
            double pK1 = 6.579 - 0.013 * temp.Value + 1.869e-4 * Math.Pow(temp.Value, 2) - 1.133e-6 * Math.Pow(temp.Value, 3) + 5.953e-9 * Math.Pow(temp.Value, 4); // corr of pK1 to temp 
            double lg_CO2 = Math.Log10(alk.ValueMEq * mol) - (pH - pK1);//  Henderson–Hasselbalch equation 
            double CO2 = Math.Pow(10, lg_CO2) * 44 * 1000; // CO2 in ppm, CO2 in conc = CO2 in feed 
            return Math.Round(CO2, 2);
        }

        // LSI index
        private double _LSI(double tds, WaterComponent temp, WaterComponent calcium, WaterComponent alk, WaterComponent pH)
        {

            double tF = 9 * temp.Value / 5 + 32;

            double pCa = -Math.Log10((calcium.ValueMEq * calcium.GetIonCharge() * mol));
            double pAlk = -Math.Log10((alk.ValueMEq * mol));
            //DuPont 1992, t here in F
            double C = 3.26 * Math.Exp(-0.005 * tF) - 0.0116 * Math.Log10(Math.Pow(tds, 3)) + 0.0905 * Math.Log10(Math.Pow(tds, 2)) - 0.133 * Math.Log10(tds) - 0.02;

            double LSI = pH.Value - (pCa + pAlk + C);
            return Math.Round(LSI, 2);
        }

        // S&DSI index
        private double _StDI(double IS, WaterComponent temp, WaterComponent calcium, WaterComponent alk, WaterComponent pH)
        {
            double K = 0;
            if (IS <= 1.2)
            {
                double power = Math.Pow((Math.Log(IS) + 7.644), 2) / 102.6;
                K = 2.022 * Math.Exp(power) - 0.0002 * Math.Pow(temp.Value, 2) + 0.00097 * temp.Value + 0.262;
            }
            else if (IS > 1.2)
            {
                K = -0.1 * IS - 0.0002 * Math.Pow(temp.Value, 2) + 0.00097 * temp.Value + 3.887;
            }
            double pCa = -Math.Log10((calcium.ValueMEq * calcium.GetIonCharge() * mol));
            double pAlk = -Math.Log10((alk.ValueMEq * mol));
            double StDI = pH.Value - (pCa + pAlk + K);

            return Math.Round(StDI, 2);
        }

        // IP CaSO4
        private double _IP_CaSO4(WaterComponent calcium, WaterComponent sulfates, double IS, WaterComponent temp)
        {
            // Ratio of solubility of monohydrate im mg/L @ given temp in deg C to solubility @ 25 deg C (611...) at which Ksp eq is calculated
            double sol_corr_t = (-0.0602986 * (temp.Value * temp.Value) + 5.65504 * temp.Value + 507.332) / 611.021375;
            if (sol_corr_t > 1) { sol_corr_t = 1; }

            // double Ksp = 8.365e-5 + 2.101e-3 * IS  - 7.09e-4 * IS * IS + 3.019e-4 * Math.pow(IS,3) - 6.969e-5 * Math.pow(IS,4);   
            double lgKsp = 1 / (-0.022 * ((Math.Log10(IS) + 2.2954 * 2.2954)) - 0.2478); //DuPont 1992
            double Ksp = Math.Pow(10, lgKsp);

            // Gettins these to to mol/L; 0.5 is the same convertion factor to mol from eq
            double IP = (mol * sulfates.ValueMEq * sulfates.GetIonCharge()) * (mol * calcium.ValueMEq * calcium.GetIonCharge());

            //supersat is calculated as ratio of actual sol product to Ksp at this ionic strength with correction on temp
            double supersat = (sol_corr_t * IP / (Ksp));
            return Math.Round(supersat, 2);
        }

        // IP BaSO4
        private double _IP_BaSO4(WaterComponent barium, WaterComponent sulfates, double IS)
        {
            double lgKsp = 1 / (-0.0226 * ((Math.Log10(IS) + 2.8747 * 2.8747) - 0.1015)); //DuPont 1992
            double Ksp = Math.Pow(10, lgKsp);
            // Gettins these to to mol/L; 0.5 is the same convertion factor to mol from eq
            double IP = (mol * sulfates.ValueMEq * sulfates.GetIonCharge()) * (mol * barium.ValueMEq * barium.GetIonCharge());
            //supersat is calculated as ratio of actual sol product to Ksp at this ionic strength with correction on temp
            double supersat = (IP / (Ksp));
            return Math.Round(supersat, 2);
        }

        // IP SrSO4
        private double _IP_SrSO4(WaterComponent strontium, WaterComponent sulfates, double IS)
        {
            double lgKsp = 1 / (-0.0079 * ((Math.Log10(IS) + 2.8154 * 2.8154) - 0.152)); //DuPont 1992
            double Ksp = Math.Pow(10, lgKsp);
            // Gettins these to to mol/L; 0.5 is the same convertion factor to mol from eq
            double IP = (mol * sulfates.ValueMEq * sulfates.GetIonCharge()) * (mol * strontium.ValueMEq * strontium.GetIonCharge());
            //supersat is calculated as ratio of actual sol product to Ksp at this ionic strength with correction on temp
            double supersat = (IP / (Ksp));
            return Math.Round(supersat, 2);
        }

        // Acid dose
        private double _pHCorrectionAcidDose(ProjectBase.EnumpHCorrection acid, double pH_adj, WaterComponent alk, WaterComponent pH, WaterComponent temp, double C_CO2)
        {
            if (acid == ProjectBase.EnumpHCorrection.None) return 0;

            double C_coef = 1;
            double dose = 0;

            switch (acid)
            {
                case ProjectBase.EnumpHCorrection.HCl:
                    // alk_coef = 1.37;
                    C_coef = 1.21;
                    break;
                case ProjectBase.EnumpHCorrection.H2SO4:
                    //alk_coef = 1.02;
                    C_coef = 0.9;
                    break;
                default:
                    // alk_coef = 0;
                    C_coef = 1;
                    break;
            }

            if (pH_adj < pH.Value)
            {
                double pK1 = 6.579 - 0.013 * temp.Value + 1.869e-4 * Math.Pow(temp.Value, 2) - 1.133e-6 * Math.Pow(temp.Value, 3) + 5.953e-9 * Math.Pow(temp.Value, 4);
                double alk_to_CO2 = Math.Exp((pH_adj - pK1) / 0.4275);
                double A = C_CO2 * alk_to_CO2 - (alk.ValueMEq * 50);
                dose = (A) / (-1 * (C_coef * alk_to_CO2));
            }
            return Math.Round(dose, 2);
        }
        #endregion
    }
}
