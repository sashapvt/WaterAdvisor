using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WaterAdvisor.Models.Project
{
    public class Water
    {
        // General
        public int Id { get; set; }

        // Cations
        public double NH4, K, Na, Ca, Mg, Fe2, Fe3, Mn, Sr, Ba;

        // Anions
        public double HCO3, SO4, Cl, NO2, NO3, F, SiO2, PO4;

        // Others
        public double pH, Temperature, Oxidability, Turbidity, TSS, Odor, Colority, Taste;
    }
}
