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
            _mgToMEq = mgToMEq;
            _type = type;
            Value = 0;
            ValueMEq = 0;
        }
        private double _mgToMEq;
        private WaterComponentType _type;

        public string Name { get; private set; }
        public double Value { get; set; }
        public double ValueMEq { get; set; }

        public double MgToMEq() { return _mgToMEq; }
        public WaterComponentType Type() { return _type; }
    }

    public enum WaterComponentType { Cation, Anion, Other };
}