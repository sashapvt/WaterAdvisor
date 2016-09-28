using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace WaterLibrary
{
    public class WaterComponent
    {
        public WaterComponent(string name, WaterComponentType type, double mgToMEq = 0, int ionCharge = 0)
        {
            _name = name;
            _type = type;
            _mgToMEq = mgToMEq;
            _ionCharge = ionCharge;
            Value = 0;
        }

        // Indexer
        public object this[string propertyName]
        {
            get { return this.GetType().GetProperty(propertyName).GetValue(this, null); }
            set { this.GetType().GetProperty(propertyName).SetValue(this, value, null); }
        }

        private string _name;
        private double _mgToMEq;
        private int _ionCharge;
        private WaterComponentType _type;

        public double Value { get; set; }

        public double ValueMEq
        {
            get
            {
                return (_mgToMEq != 0) ? Math.Round(Value / _mgToMEq, 2) : 0;
            }
            set
            {
                if (_mgToMEq != 0) Value = Math.Round(value * _mgToMEq, 2);
            }
        }

        public string GetName() { return _name; }
        public double GetMgToMEq() { return _mgToMEq; }
        public int GetIonCharge() { return _ionCharge; }
        public WaterComponentType GetComponentType() { return _type; }
    }

    public enum WaterComponentType { Cation, Anion, Other };
}