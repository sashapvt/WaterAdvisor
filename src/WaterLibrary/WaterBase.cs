using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WaterLibrary
{
    public class WaterBase
    {
        // General
        public int Id { get; set; }

        // Cations
        public double NH4 { get; set; }
        public double K { get; set; }
        public double Na { get; set; }
        public double Ca { get; set; }
        public double Mg { get; set; }
        public double Fe2 { get; set; }
        public double Fe3 { get; set; }
        public double Mn { get; set; }
        public double Sr { get; set; }
        public double Ba { get; set; }

        // Anions
        public double HCO3 { get; set; }
        public double SO4 { get; set; }
        public double Cl { get; set; }
        public double NO2 { get; set; }
        public double NO3 { get; set; }
        public double F { get; set; }
        public double SiO2 { get; set; }
        public double PO4 { get; set; }

        // Others
        public double pH { get; set; }
        public double Temperature { get; set; }
        public double Oxidability { get; set; }
        public double Turbidity { get; set; }
        public double TSS { get; set; }
        public double Odor { get; set; }
        public double Colority { get; set; }
        public double Taste { get; set; }
    }
}
