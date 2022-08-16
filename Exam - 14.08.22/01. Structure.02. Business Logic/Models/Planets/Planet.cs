using PlanetWars.Models.MilitaryUnits.Contracts;
using PlanetWars.Models.Planets.Contracts;
using PlanetWars.Models.Weapons.Contracts;
using PlanetWars.Repositories;
using PlanetWars.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlanetWars.Models.Planets
{
    public class Planet : IPlanet
    {
        private UnitRepository units;
        private WeaponRepository weapons;
        private string name;
        private double budget;

        public Planet(string name, double budget)
        {
            units = new UnitRepository();
            weapons = new WeaponRepository();
            this.Name = name;
            this.Budget = budget;
        }
        public string Name
        {
            get => name;
           private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.InvalidPlanetName);
                }
                name = value;
            }
        }

        public double Budget
        {
            get => budget;
           private set
            {
                if (value < 0)
                {
                    throw new ArgumentException(ExceptionMessages.InvalidBudgetAmount);
                }
                budget = value;
            }
        }

        public double MilitaryPower =>  GetMilitaryPower();

        public IReadOnlyCollection<IMilitaryUnit> Army => units.Models;

        public IReadOnlyCollection<IWeapon> Weapons => weapons.Models;

        public void AddUnit(IMilitaryUnit unit)
        {
           units.AddItem(unit);
        }

        public void AddWeapon(IWeapon weapon)
        {
            weapons.AddItem(weapon);
        }

        public string PlanetInfo()
        {//string force = string.Empty;
            //string combat = string.Empty;           
            //foreach (var item in units.Models)
            //{               
            //   force = item.GetType().Name == null ? "No units" : string.Join(", ", item.GetType().Name);
            //}            
            //foreach (var item in weapons.Models)
            //{
            //    force = item.GetType().Name == null ? "No weapons" : string.Join(", ", item.GetType().Name);
            //}
            string forcesAsString = Army.Any() 
                ? string.Join(", ", Army.Select(u => u.GetType().Name)) 
                : "No units";
            string weaponsAsString = Weapons.Any() 
                ? string.Join(", ", Weapons.Select(w => w.GetType().Name)) 
                : "No weapons";
            
            StringBuilder sb = new StringBuilder();
            
            sb.AppendLine($"Planet: {Name}");
            sb.AppendLine($"--Budget: {Budget} billion QUID");
            sb.AppendLine($"--Forces: {forcesAsString}");
            sb.AppendLine($"--Combat equipment: {weaponsAsString}");
            sb.AppendLine($"--Military Power: {MilitaryPower}");

            return sb.ToString().TrimEnd();
        }

        public void Profit(double amount)
        {
            Budget += amount;
        }

        public void Spend(double amount)
        {
            if (Budget < amount)
            {
                throw new InvalidOperationException(ExceptionMessages.UnsufficientBudget);
            }

            Budget -= amount;
        }

        public void TrainArmy()
        {
            foreach (var unit in Army)
            {
                unit.IncreaseEndurance();
            }
        }

        private double GetMilitaryPower()
        {
            double total = this.Weapons.Sum(w => w.DestructionLevel) + this.Army.Sum(u => u.EnduranceLevel);

            if (Army.Any(u => u.GetType().Name == "AnonymousImpactUnit"))
            {
                total *= 1.3;
            }

            if (Weapons.Any(w => w.GetType().Name == "NuclearWeapon"))
            {
                total *= 1.45;
            }

            return Math.Round(total, 3);
        }
    }
}
