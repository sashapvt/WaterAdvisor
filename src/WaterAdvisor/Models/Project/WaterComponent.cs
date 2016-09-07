﻿using System;
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
            _name = name;
            _mgToMEq = mgToMEq;
            _type = type;
            Value = 0;
        }
        private string _name;
        private double _mgToMEq;
        private WaterComponentType _type;

        public double Value { get; set; }

        public double ValueMEq
        {
            get
            {
                return (_mgToMEq != 0) ? Math.Round(Value / _mgToMEq, 3) : 0;
            }
            set
            {
                if (_mgToMEq != 0) Value = Math.Round(value * _mgToMEq, 3);
            }
        }

        public string GetName() { return _name; }
        public double GetMgToMEq() { return _mgToMEq; }
        public WaterComponentType GetComponentType() { return _type; }
    }

    public enum WaterComponentType { Cation, Anion, Other };
}