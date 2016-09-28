using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WaterLibrary
{
    public class RosaParser
    {
        public static bool ParseRosa(string textROSA, WaterBase waterIn, out double recovery)
        {
            int index;
            // Test
            textROSA = "Project Information:  Case-specific: Вар. 1. Стандартный расчет System Details -- Pass 1 Feed Flow to Stage 1 71.67 m³/h  Pass 1 Permeate Flow 50.00 m³/h  Osmotic Pressure:    Raw Water Flow to System 66.67 m³/h  Pass 1 Recovery 75.00 %  Feed 0.17 bar  Feed Pressure 6.11 bar  Feed Temperature 20.0 C  Concentrate 0.62 bar  Flow Factor 0.85   Feed TDS 377.00 mg/l  Average 0.39 bar  Chem. Dose  None   Number of Elements 60   Average NDP 4.25 bar  Total Active Area 2452.56 M²  Average Pass 1 Flux 20.39 lmh  Power 17.37 kW  Water Classification: Surface Supply SDI < 5      Specific Energy 0.35 kWh/m³  System Recovery 56.24 %      Conc. Flow from Pass 2 0.00 m³/h  Stage Element #PV #Ele Feed Flow (m³/h)  Feed Press (bar)  Recirc Flow (m³/h)  Conc Flow (m³/h)  Conc Press (bar)  Perm Flow (m³/h)  Avg Flux (lmh)  Perm Press (bar)  Boost Press (bar)  Perm TDS (mg/l)   1 XLE-440 7 5 71.67 5.76 5.00 37.74 4.58 33.93 23.71 0.50 0.00 12.39  2 XLE-440 5 5 37.74 4.24 0.00 21.67 3.44 16.07 15.73 0.50 0.00 28.67  Pass Streams (mg/l as Ion)   Name Feed Adjusted Feed Concentrate  Permeate   Initial After Recycles Stage 1 Stage 2 Stage 1 Stage 2 Total  NH4+ + NH3 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00  K 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00  Na 11.78 11.78 13.94 25.48 42.61 1.11 2.39 1.52  Mg 12.20 12.20 14.67 27.58 47.50 0.30 0.73 0.44  Ca 69.14 69.14 83.13 156.36 269.34 1.67 4.09 2.45  Sr 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00  Ba 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00  CO3 0.14 0.14 0.21 0.81 2.61 0.00 0.00 0.00  HCO3 207.42 207.42 249.39 468.49 804.89 4.93 11.87 7.13  NO3 8.30 8.30 9.25 14.89 21.48 2.97 6.01 3.94  Cl 24.00 24.02 28.83 54.08 92.80 0.75 1.89 1.11  F 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00  SO4 36.00 36.00 43.39 81.96 141.87 0.49 1.22 0.72  SiO2 8.00 8.00 9.62 18.10 31.18 0.18 0.48 0.28  Boron 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00  CO2 29.60 29.60 29.60 29.86 30.61 29.50 29.97 29.67  TDS 376.98 377.00 452.43 847.75 1454.29 12.39 28.67 17.60  pH 7.00 7.00 7.07 7.31 7.51 5.46 5.82 5.61  *Permeate Flux reported by ROSA is calculated based on ACTIVE membrane area. DISCLAIMER: NO WARRANTY, EXPRESSED OR IMPLIED, AND NO WARRANTY OF MERCHANTABILITY OR FITNESS FOR A PARTICULAR PURPOSE, IS GIVEN. Neither FilmTec Corporation nor The Dow Chemical Company assume any obligation or liability for results obtained or damages incurred from the application of this information. Because use conditions and applicable laws may differ from one location to another and may change with time, customer is responsible for determining whether products are appropriate for customer’s use. FilmTec Corporation and The Dow Chemical Company assume no liability, if, as a result of customer's use of the ROSA membrane design software, the customer should be sued for alleged infringement of any patent not owned or controlled by the FilmTec Corporation nor The Dow Chemical Company. Reverse Osmosis System Analysis for FILMTEC™ Membranes ROSA 9.1 ConfigDB U399339_349  Project: ST-1 Case: 1  Sasha, Ecosoft 3/9/2015  Design Warnings -- Pass 1 -None- ";

            waterIn.Ca = 20;
            recovery = 80;

            //foreach(string s in textROSA.Split())
            //{
            //    Console.WriteLine(s);
            //}
            //index = textROSA.IndexOf("Recovery ");
            //recovery = Double.Parse(textROSA.Substring(index + 9, textROSA.IndexOf(" ")));

            return true;
        }
    }
}
