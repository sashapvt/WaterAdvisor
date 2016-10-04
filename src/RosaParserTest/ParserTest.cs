using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WaterLibrary;

namespace RosaParserTest
{
    public class ParserTest
    {
        public static void Main(string[] args)
        {
            #region var textROSA
            string textROSA = @"Project Information: 

Case-specific: 

System Details -- Pass 1

Feed Flow to Stage 1 26.00 m³/h  Pass 1 Permeate Flow 18.00 m³/h  Osmotic Pressure:   
Raw Water Flow to System 24.00 m³/h  Pass 1 Recovery 74.99 %  Feed 0.33 bar 
Feed Pressure 6.76 bar  Feed Temperature 25.0 C  Concentrate 1.21 bar 
Flow Factor 0.85   Feed TDS 680.09 mg/l  Average 0.77 bar 
Chem. Dose  None   Number of Elements 18   Average NDP 5.08 bar 
Total Active Area 735.77 M²  Average Pass 1 Flux 24.46 lmh  Power 6.10 kW 
Water Classification: Surface Water with DOW Ultrafiltration, SDI<2.5      Specific Energy 0.34 kWh/m³ 
System Recovery 67.50 %      Conc. Flow from Pass 2 0.00 m³/h 

Stage Element #PV #Ele Feed Flow
(m³/h)  Feed Press
(bar)  Recirc Flow
(m³/h)  Conc Flow
(m³/h)  Conc Press
(bar)  Perm Flow
(m³/h)  Avg Flux
(lmh)  Perm Press
(bar)  Boost Press
(bar)  Perm TDS
(mg/l)  
1 BW30HRLE-440i 3 4 26.00 6.42 2.00 12.89 5.74 13.11 26.72 0.50 0.00 13.55 
2 BW30HRLE-440i 2 3 12.89 5.39 0.00 8.00 5.02 4.89 19.94 0.50 0.00 31.05 


Pass Streams
(mg/l as Ion)  
Name Feed Adjusted Feed Concentrate  Permeate  
Initial After Recycles Stage 1 Stage 2 Stage 1 Stage 2 Total 
NH4+ + NH3 0.28 0.31 0.37 0.76 1.18 0.05 0.08 0.06 
K 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 
Na 43.07 43.19 52.78 105.08 167.42 1.33 3.08 1.81 
Mg 24.31 24.31 29.81 59.73 95.69 0.39 0.89 0.52 
Ca 104.21 104.21 127.80 256.10 410.34 1.61 3.72 2.18 
Sr 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 
Ba 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 
CO3 7.21 7.21 9.77 24.05 43.64 0.00 0.01 0.00 
HCO3 414.83 414.83 506.30 1003.52 1594.70 8.80 20.10 11.87 
NO3 8.20 8.20 9.89 19.23 29.99 0.71 1.61 0.95 
Cl 41.00 41.00 50.33 101.03 162.11 0.47 1.09 0.64 
F 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 
SO4 36.80 36.80 45.23 90.99 146.29 0.22 0.51 0.30 
SiO2 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 
Boron 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 
CO2 2.61 2.61 2.98 5.51 8.72 3.45 5.97 4.13 
TDS 679.94 680.09 832.32 1660.50 2651.36 13.55 31.05 18.30 
pH 8.30 8.30 8.32 8.31 8.28 6.61 6.72 6.66 


*Permeate Flux reported by ROSA is calculated based on ACTIVE membrane area. DISCLAIMER: NO WARRANTY, EXPRESSED OR IMPLIED, AND NO WARRANTY OF MERCHANTABILITY OR FITNESS FOR A PARTICULAR PURPOSE, IS GIVEN. Neither FilmTec Corporation nor The Dow Chemical Company assume any obligation or liability for results obtained or damages incurred from the application of this information. Because use conditions and applicable laws may differ from one location to another and may change with time, customer is responsible for determining whether products are appropriate for customer’s use. FilmTec Corporation and The Dow Chemical Company assume no liability, if, as a result of customer's use of the ROSA membrane design software, the customer should be sued for alleged infringement of any patent not owned or controlled by the FilmTec Corporation nor The Dow Chemical Company.

Reverse Osmosis System Analysis for FILMTEC™ Membranes ROSA 9.1 ConfigDB U399339_349 
Project: TEC-17-7 Case: 1 
,  9/28/2016 

Design Warnings -- Pass 1

-None- 
";
# endregion
            WaterBase waterIn = new WaterBase();
            double recovery;
            var RosaResult = RosaParser.ParseRosa(textROSA, waterIn, out recovery);
            if (RosaResult)
            {
                // Recovery
                Console.WriteLine("Recovery = {0}%", recovery);
                // Cations
                Console.WriteLine("NH4 = {0}", waterIn.NH4);
                Console.WriteLine("K = {0}", waterIn.K);
                Console.WriteLine("Na = {0}", waterIn.Na);
                Console.WriteLine("Ca = {0}", waterIn.Ca);
                Console.WriteLine("Mg = {0}", waterIn.Mg);
                Console.WriteLine("Sr = {0}", waterIn.Sr);
                Console.WriteLine("Ba = {0}", waterIn.Ba);
                // Anions
                Console.WriteLine("HCO3 = {0}", waterIn.HCO3);
                Console.WriteLine("SO4 = {0}", waterIn.SO4);
                Console.WriteLine("Cl = {0}", waterIn.Cl);
                Console.WriteLine("NO3 = {0}", waterIn.NO3);
                Console.WriteLine("F = {0}", waterIn.F);
                Console.WriteLine("SiO2 = {0}", waterIn.SiO2);
                // Others
                Console.WriteLine("pH = {0}", waterIn.pH);
                Console.WriteLine("Temperature = {0}", waterIn.Temperature);
            }
            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }
    }
}
