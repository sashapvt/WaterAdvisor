using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Globalization;

namespace WaterLibrary
{
    public class RosaParser
    {
        public static bool ParseRosa(string textROSA, WaterBase waterIn, out double recovery)
        {
            try
            {
                string[] textROSALines = Regex.Split(textROSA, "\r\n|\r|\n");
                var t = textROSALines.Where(s => s.StartsWith("Raw Water")).SingleOrDefault();
                recovery = Double.Parse(Regex.Match(t, @"Recovery ([0-9\.]+) %").Groups[1].Value, CultureInfo.InvariantCulture);
                t = textROSALines.Where(s => s.StartsWith("Feed Pressure")).SingleOrDefault();
                waterIn.Temperature = Double.Parse(Regex.Match(t, @"Temperature ([0-9\.]+) C").Groups[1].Value, CultureInfo.InvariantCulture);
                int index = -1;
                for (int i = 10; i < textROSALines.Length; i++) if (textROSALines[i].StartsWith("NH4")) index = i;
                if (index != -1)
                {
                    waterIn.NH4 = ReadDouble(textROSALines[index], 4);
                    waterIn.K = ReadDouble(textROSALines[index + 1], 2);
                    waterIn.Na = ReadDouble(textROSALines[index + 2], 2);
                    waterIn.Mg = ReadDouble(textROSALines[index + 3], 2);
                    waterIn.Ca = ReadDouble(textROSALines[index + 4], 2);
                    waterIn.Sr = ReadDouble(textROSALines[index + 5], 2);
                    waterIn.Ba = ReadDouble(textROSALines[index + 6], 2);
                    waterIn.HCO3 = ReadDouble(textROSALines[index + 8], 2);
                    waterIn.NO3 = ReadDouble(textROSALines[index + 9], 2);
                    waterIn.Cl = ReadDouble(textROSALines[index + 10], 2);
                    waterIn.F = ReadDouble(textROSALines[index + 11], 2);
                    waterIn.SO4 = ReadDouble(textROSALines[index + 12], 2);
                    waterIn.SiO2 = ReadDouble(textROSALines[index + 13], 2);
                    waterIn.pH = ReadDouble(textROSALines[index + 17], 2);
                    waterIn.Fe2 = 0;
                    waterIn.Fe3 = 0;
                    waterIn.Mn = 0;
                    waterIn.PO4 = 0;
                    return true;
                }
                else
                    return false;
            }
            catch
            {
                recovery = 0;
                return false;
            }
        }
        private static double ReadDouble(string s, int pos)
        {
            return Double.Parse(s.Split()[pos], CultureInfo.InvariantCulture);
        }
    }
}
