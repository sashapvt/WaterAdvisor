using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace WaterLibrary
{
    public class WaterList
    {
        public WaterList()
        {
            // Cations
            NH4 = new WaterComponent("Аммоній", WaterComponentType.Cation, 18.00, 1);
            K = new WaterComponent("Калій", WaterComponentType.Cation, 39.10, 1);
            Na = new WaterComponent("Натрій", WaterComponentType.Cation, 23.00, 1);
            Ca = new WaterComponent("Кальцій", WaterComponentType.Cation, 20.04, 2);
            Mg = new WaterComponent("Магній", WaterComponentType.Cation, 12.15, 2);
            Fe2 = new WaterComponent("Залізо 2+", WaterComponentType.Cation, 27.93, 2);
            Fe3 = new WaterComponent("Залізо 3+", WaterComponentType.Cation, 18.62, 3);
            Mn = new WaterComponent("Марганець", WaterComponentType.Cation, 27.47, 2);
            Sr = new WaterComponent("Стронцій", WaterComponentType.Cation, 43.81, 2);
            Ba = new WaterComponent("Барій", WaterComponentType.Cation, 68.66, 2);

            // Anions
            HCO3 = new WaterComponent("Гідрокарбонати", WaterComponentType.Anion, 61.00, 1);
            SO4 = new WaterComponent("Сульфати", WaterComponentType.Anion, 48.00, 2);
            Cl = new WaterComponent("Хлориди", WaterComponentType.Anion, 35.45, 1);
            NO2 = new WaterComponent("Нітрити", WaterComponentType.Anion, 46.00, 1);
            NO3 = new WaterComponent("Нітрати", WaterComponentType.Anion, 62.00, 1);
            F = new WaterComponent("Фториди", WaterComponentType.Anion, 19.00, 1);
            SiO2 = new WaterComponent("Силікати", WaterComponentType.Anion, 0, 4);
            PO4 = new WaterComponent("Фосфати", WaterComponentType.Anion, 31.66, 3);

            // Others
            pH = new WaterComponent("pH", WaterComponentType.Other);
            Temperature = new WaterComponent("Температура", WaterComponentType.Other);
            Oxidability = new WaterComponent("Окисність", WaterComponentType.Other);
            Turbidity = new WaterComponent("Мутність", WaterComponentType.Other);
            TSS = new WaterComponent("Зважені речовини", WaterComponentType.Other);
            Odor = new WaterComponent("Запах", WaterComponentType.Other);
            Colority = new WaterComponent("Кольоровість", WaterComponentType.Other);
            Taste = new WaterComponent("Присмак", WaterComponentType.Other);

            //Lists
            _cations = new List<WaterComponent> { NH4, K, Na, Ca, Mg, Fe2, Fe3, Mn, Sr, Ba };
            _anions = new List<WaterComponent> { HCO3, SO4, Cl, NO2, NO3, F, SiO2, PO4 };
        }

        // Indexer
        public object this[string propertyName]
        {
            get { return this.GetType().GetProperty(propertyName).GetValue(this, null); }
            set { this.GetType().GetProperty(propertyName).SetValue(this, value, null); }
        }

        // Private lists
        private List<WaterComponent> _cations;
        private List<WaterComponent> _anions;

        // Cations
        public WaterComponent NH4 { get; private set; }
        public WaterComponent K { get; private set; }
        public WaterComponent Na { get; private set; }
        public WaterComponent Ca { get; private set; }
        public WaterComponent Mg { get; private set; }
        public WaterComponent Fe2 { get; private set; }
        public WaterComponent Fe3 { get; private set; }
        public WaterComponent Mn { get; private set; }
        public WaterComponent Sr { get; private set; }
        public WaterComponent Ba { get; private set; }

        // Anions
        public WaterComponent HCO3 { get; private set; }
        public WaterComponent SO4 { get; private set; }
        public WaterComponent Cl { get; private set; }
        public WaterComponent NO2 { get; private set; }
        public WaterComponent NO3 { get; private set; }
        public WaterComponent F { get; private set; }
        public WaterComponent SiO2 { get; private set; }
        public WaterComponent PO4 { get; private set; }

        // Others
        public WaterComponent pH { get; private set; }
        public WaterComponent Temperature { get; private set; }
        public WaterComponent Oxidability { get; private set; }
        public WaterComponent Turbidity { get; private set; }
        public WaterComponent TSS { get; private set; }
        public WaterComponent Odor { get; private set; }
        public WaterComponent Colority { get; private set; }
        public WaterComponent Taste { get; private set; }

        // Lists
        public List<WaterComponent> Cations() { return _cations; }
        public List<WaterComponent> Anions() { return _anions; }

        // Callculated properties
        public double SumCationsMEq => Math.Round(Cations().Sum(x => x.ValueMEq), 2);
        public double SumAnionsMEq => Math.Round(Anions().Sum(x => x.ValueMEq), 2);
        public double SumIonsBalance => Math.Round(SumCationsMEq - SumAnionsMEq, 2);
        public double TDS => Math.Round(Cations().Sum(x => x.Value) + Anions().Sum(x => x.Value), 2);

        // Import to Water model
        public void ImportWater(WaterBase water)
        {
            // Cations
            NH4.Value = water.NH4;
            K.Value = water.K;
            Na.Value = water.Na;
            Ca.Value = water.Ca;
            Mg.Value = water.Mg;
            Fe2.Value = water.Fe2;
            Fe3.Value = water.Fe3;
            Mn.Value = water.Mn;
            Sr.Value = water.Sr;
            Ba.Value = water.Ba;

            // Anions
            HCO3.Value = water.HCO3;
            SO4.Value = water.SO4;
            Cl.Value = water.Cl;
            NO2.Value = water.NO2;
            NO3.Value = water.NO3;
            F.Value = water.F;
            SiO2.Value = water.SiO2;
            PO4.Value = water.PO4;

            // Others
            pH.Value = water.pH;
            Temperature.Value = water.Temperature;
            Oxidability.Value = water.Oxidability;
            Turbidity.Value = water.Turbidity;
            TSS.Value = water.TSS;
            Odor.Value = water.Odor;
            Colority.Value = water.Colority;
            Taste.Value = water.Taste;
        }

        // Export to Water model
        public void ExportWater(WaterBase water)
        {
            // Cations
            water.NH4 = NH4.Value;
            water.K = K.Value;
            water.Na = Na.Value;
            water.Ca = Ca.Value;
            water.Mg = Mg.Value;
            water.Fe2 = Fe2.Value;
            water.Fe3 = Fe3.Value;
            water.Mn = Mn.Value;
            water.Sr = Sr.Value;
            water.Ba = Ba.Value;

            // Anions
            water.HCO3 = HCO3.Value;
            water.SO4 = SO4.Value;
            water.Cl = Cl.Value;
            water.NO2 = NO2.Value;
            water.NO3 = NO3.Value;
            water.F = F.Value;
            water.SiO2 = SiO2.Value;
            water.PO4 = PO4.Value;

            // Others
            water.pH = pH.Value;
            water.Temperature = Temperature.Value;
            water.Oxidability = Oxidability.Value;
            water.Turbidity = Turbidity.Value;
            water.TSS = TSS.Value;
            water.Odor = Odor.Value;
            water.Colority = Colority.Value;
            water.Taste = Taste.Value;
        }
    }
}
