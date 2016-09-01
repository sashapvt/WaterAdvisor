using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WaterAdvisor.Models.Project
{
    public class WaterList
    {
        public WaterList()
        {
            // Cations
            NH4 = new WaterComponent("Аммоній", 18.00, WaterComponentType.Cation);
            K = new WaterComponent("Калій", 39.10, WaterComponentType.Cation);
            Na = new WaterComponent("Натрій", 23.00, WaterComponentType.Cation);
            Ca = new WaterComponent("Кальцій", 20.04, WaterComponentType.Cation);
            Mg = new WaterComponent("Магній", 12.15, WaterComponentType.Cation);
            Fe2 = new WaterComponent("Залізо 2+", 27.93, WaterComponentType.Cation);
            Fe3 = new WaterComponent("Залізо 3+", 18.62, WaterComponentType.Cation);
            Mn = new WaterComponent("Марганець", 54.94, WaterComponentType.Cation);
            Sr = new WaterComponent("Стронцій", 43.81, WaterComponentType.Cation);
            Ba = new WaterComponent("Барій", 68.66, WaterComponentType.Cation);

            // Anions
            HCO3 = new WaterComponent("Гідрокарбонати", 61.00, WaterComponentType.Anion);
            SO4 = new WaterComponent("Сульфати", 48.00, WaterComponentType.Anion);
            Cl = new WaterComponent("Хлориди", 35.45, WaterComponentType.Anion);
            NO2 = new WaterComponent("Нітрити", 46.00, WaterComponentType.Anion);
            NO3 = new WaterComponent("Нітрати", 62.00, WaterComponentType.Anion);
            F = new WaterComponent("Фториди", 19.00, WaterComponentType.Anion);
            SiO2 = new WaterComponent("Силікати", 0, WaterComponentType.Anion);
            PO4 = new WaterComponent("Фосфати", 31.66, WaterComponentType.Anion);

            // Others
            pH = new WaterComponent("pH", 0, WaterComponentType.Other);
            Temperature = new WaterComponent("Температура", 0, WaterComponentType.Other);
            Oxidability = new WaterComponent("Окисність", 0, WaterComponentType.Other);
            Turbidity = new WaterComponent("Мутність", 0, WaterComponentType.Other);
            TSS = new WaterComponent("Зважені речовини", 0, WaterComponentType.Other);
            Odor = new WaterComponent("Запах", 0, WaterComponentType.Other);
            Colority = new WaterComponent("Кольоровість", 0, WaterComponentType.Other);
            Taste = new WaterComponent("Присмак", 0, WaterComponentType.Other);

            //Lists
            _cations = new List<WaterComponent> { NH4, K, Na, Ca, Mg, Fe2, Fe3, Mn, Sr, Ba };
            _anions = new List<WaterComponent> { HCO3, SO4, Cl, NO2, NO3, F, SiO2, PO4 };
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

        // Import to Water model
        public void ImportWater(Water water)
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
        public Water ExportWater()
        {
            var water = new Water();

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

            return water;
        }
    }
}
