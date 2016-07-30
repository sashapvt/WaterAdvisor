using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WaterAdvisor.Models.Project
{
    public class WaterComponent
    {
        public WaterComponent(string name, double mgToMEq, WaterComponentType type)
        {
            Name = name;
            MgToMEq = mgToMEq;
            Type = type;
            Value = 0;
            ValueMEq = 0;
        }

        public string Name { get; private set; }
        public double Value { get; set; }
        public double ValueMEq { get; set; }
        public double MgToMEq { get; private set; }
        public WaterComponentType Type { get; private set; }
    }

    public enum WaterComponentType { Cation, Anion, Other };
}