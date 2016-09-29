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
            //int index;
            // Test

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
