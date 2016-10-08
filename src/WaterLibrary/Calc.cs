using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WaterLibrary
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

        // pH 
        public double pH => (p.pHCorrection == ProjectBase.EnumpHCorrection.None) ? w.pH.Value : p.pHCorrected; // Feed water
        public double pH_c => _pH_c(w.Temperature.Value, w.HCO3, pH, p.GetCF(), CO2); // Concentrate
        // IS (Ionic streight)
        public double IS => _IS(w); // Feed water
        public double IS_c => _IS(w) * p.GetCF(); // Concentrate
        // CO2 concentration
        public double CO2 => _CO2(w.Temperature, w.HCO3, pH);
        public double CO2_c => _CO2(w.Temperature, w.HCO3, pH);
        // LSI index
        public double LSI => _LSI(w.TDS, w.Temperature, w.Ca, w.HCO3, w.pH);
        public double LSI_c => _LSI(w.TDS, w.Temperature, w.Ca, w.HCO3, w.pH, p.GetCF());
        // S&DSI index
        public double StDI => _StDI(IS, w.Temperature, w.Ca, w.HCO3, w.pH);
        public double StDI_c => _StDI(IS, w.Temperature, w.Ca, w.HCO3, w.pH, p.GetCF());
        // IP CaSO4
        public double IP_CaSO4 => _IP_CaSO4(w.Ca, w.SO4, IS, w.Temperature);
        public double IP_CaSO4_c => _IP_CaSO4(w.Ca, w.SO4, IS_c, w.Temperature, p.GetCF());
        // IP BaSO4
        public double IP_BaSO4 => _IP_BaSO4(w.Ba, w.SO4, IS);
        public double IP_BaSO4_c => _IP_BaSO4(w.Ba, w.SO4, IS_c, p.GetCF());
        // IP SrSO4
        public double IP_SrSO4 => _IP_SrSO4(w.Sr, w.SO4, IS);
        public double IP_SrSO4_c => _IP_SrSO4(w.Sr, w.SO4, IS_c, p.GetCF());
        // Acid dose for pH correction, mg/l
        public double pHCorrectionAcidDose => _pHCorrectionAcidDose(p.pHCorrection, p.pHCorrected, w.HCO3, w.pH, w.Temperature, _CO2(w.Temperature, w.HCO3, p.pHCorrected));
        // Ecotec RO 1000 dose
        public double EcotecRO1000 => _EcotecRO1000(LSI_c, IP_CaSO4_c, p.GetCF());

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

        // pH of concentrate
        private double _pH_c(double temp, WaterComponent alk, double pH, double CF, double C_CO2)
        {
            double A = 1 / (44.0 * 1000); // reciprocal of M_CO2 multiplied by conversion to mol/L
            double pK1 = 6.579 - 0.013 * temp + 1.869e-4 * Math.Pow(temp, 2) - 1.133e-6 * Math.Pow(temp, 3) + 5.953e-9 * Math.Pow(temp, 4); // corr of pK1 to temp 
            double lg_CO2 = Math.Log10(C_CO2 * A);
            double pH_c = pK1 + Math.Log10(CF * alk.ValueMEq * mol) - lg_CO2; //  Henderson–Hasselbalch equation 
            return Math.Round(pH_c, 2);
        }

        // LSI index
        private double _LSI(double tds, WaterComponent temp, WaterComponent calcium, WaterComponent alk, WaterComponent pH, double CF = 1)
        {

            double tF = 9 * temp.Value / 5 + 32;

            double pCa = -Math.Log10((calcium.ValueMEq * calcium.GetIonCharge() * mol * CF));
            double pAlk = -Math.Log10((alk.ValueMEq * mol * CF));
            //DuPont 1992, t here in F
            double C = 3.26 * Math.Exp(-0.005 * tF) - 0.0116 * Math.Log10(Math.Pow(tds, 3)) + 0.0905 * Math.Log10(Math.Pow(tds, 2)) - 0.133 * Math.Log10(tds) - 0.02;

            double LSI = pH.Value - (pCa + pAlk + C);
            return Math.Round(LSI, 2);
        }

        // S&DSI index
        private double _StDI(double IS, WaterComponent temp, WaterComponent calcium, WaterComponent alk, WaterComponent pH, double CF = 1)
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
            double pCa = -Math.Log10((calcium.ValueMEq * calcium.GetIonCharge() * mol * CF));
            double pAlk = -Math.Log10((alk.ValueMEq * mol * CF));
            double StDI = pH.Value - (pCa + pAlk + K);

            return Math.Round(StDI, 2);
        }

        // IP CaSO4
        private double _IP_CaSO4(WaterComponent calcium, WaterComponent sulfates, double IS, WaterComponent temp, double CF = 1)
        {
            // Ratio of solubility of monohydrate im mg/L @ given temp in deg C to solubility @ 25 deg C (611...) at which Ksp eq is calculated
            double sol_corr_t = (-0.0602986 * (temp.Value * temp.Value) + 5.65504 * temp.Value + 507.332) / 611.021375;
            if (sol_corr_t > 1) { sol_corr_t = 1; }

            // double Ksp = 8.365e-5 + 2.101e-3 * IS  - 7.09e-4 * IS * IS + 3.019e-4 * Math.pow(IS,3) - 6.969e-5 * Math.pow(IS,4);   
            double lgKsp = 1 / (-0.022 * ((Math.Log10(IS) + 2.2954 * 2.2954)) - 0.2478); //DuPont 1992
            double Ksp = Math.Pow(10, lgKsp);

            // Gettins these to to mol/L; 0.5 is the same convertion factor to mol from eq
            double IP = (CF * mol * sulfates.ValueMEq * sulfates.GetIonCharge()) * (CF * mol * calcium.ValueMEq * calcium.GetIonCharge());

            //supersat is calculated as ratio of actual sol product to Ksp at this ionic strength with correction on temp
            double supersat = (sol_corr_t * IP / (Ksp));
            return Math.Round(supersat, 2);
        }

        // IP BaSO4
        private double _IP_BaSO4(WaterComponent barium, WaterComponent sulfates, double IS, double CF = 1)
        {
            double lgKsp = 1 / (-0.0226 * ((Math.Log10(IS) + 2.8747 * 2.8747) - 0.1015)); //DuPont 1992
            double Ksp = Math.Pow(10, lgKsp);
            // Gettins these to to mol/L; 0.5 is the same convertion factor to mol from eq
            double IP = (CF * mol * sulfates.ValueMEq * sulfates.GetIonCharge()) * (CF * mol * barium.ValueMEq * barium.GetIonCharge());
            //supersat is calculated as ratio of actual sol product to Ksp at this ionic strength with correction on temp
            double supersat = (IP / (Ksp));
            return Math.Round(supersat, 2);
        }

        // IP SrSO4
        private double _IP_SrSO4(WaterComponent strontium, WaterComponent sulfates, double IS, double CF = 1)
        {
            double lgKsp = 1 / (-0.0079 * ((Math.Log10(IS) + 2.8154 * 2.8154) - 0.152)); //DuPont 1992
            double Ksp = Math.Pow(10, lgKsp);
            // Gettins these to to mol/L; 0.5 is the same convertion factor to mol from eq
            double IP = (CF * mol * sulfates.ValueMEq * sulfates.GetIonCharge()) * (CF * mol * strontium.ValueMEq * strontium.GetIonCharge());
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

        // Ecotec RO 1000 dose calculation
        private double _EcotecRO1000(double lsi, double IP_CaSO4, double CF)
        {
            // LSI based calculation
            double calculated_dose_lsi = (-29.8 + 16.1 * lsi) / 0.3;
            if (lsi > 2.7)
            {
                calculated_dose_lsi = 0;
            }
            else if (calculated_dose_lsi < (16.7 / CF))
            {
                calculated_dose_lsi = 16.7 / CF;
            }
            // else calculated_dose_lsi = calculated_dose_lsi / CF;

            // IP CaSO4 based calculation
            double calculated_dose_IP = (-899.576 + 1.548e3 * IP_CaSO4 - 1.051e3 * Math.Pow(IP_CaSO4, 2) + 353.267 * Math.Pow(IP_CaSO4, 3) - 58.557 * Math.Pow(IP_CaSO4, 4) + 3.828 * Math.Pow(IP_CaSO4, 5)) / 0.3;
            //(-29.8 + 16.1 * IP)/ CF / 0.3;
            if (IP_CaSO4 > 3.5)
            {
                calculated_dose_IP = 0;
            }
            else if (calculated_dose_IP < (16.7 / CF))
            {
                calculated_dose_IP =  16.7 / CF;
            }
            // else calculated_dose_IP = calculated_dose_IP / CF;

            // Return max calculated value
            return Math.Round(Math.Max(calculated_dose_lsi, calculated_dose_IP), 1);
        }
        #endregion
    }
}
