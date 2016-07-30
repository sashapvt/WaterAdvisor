using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WaterAdvisor.Models.Project
{
    public class Calc
    {
        public Calc()
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
            Cations = new List<WaterComponent> { NH4, K, Na, Ca, Mg, Fe2, Fe3, Mn, Sr, Ba };
            Anions = new List<WaterComponent> { HCO3, SO4, Cl, NO2, NO3, F, SiO2, PO4 };
        }

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
        public List<WaterComponent> Cations;
        public List<WaterComponent> Anions;
    }
}
