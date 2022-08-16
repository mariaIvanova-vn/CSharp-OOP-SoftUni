using PlanetWars.Core.Contracts;
using PlanetWars.Models.MilitaryUnits;
using PlanetWars.Models.MilitaryUnits.Contracts;
using PlanetWars.Models.Planets;
using PlanetWars.Models.Planets.Contracts;
using PlanetWars.Models.Weapons;
using PlanetWars.Models.Weapons.Contracts;
using PlanetWars.Repositories;
using PlanetWars.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlanetWars.Core
{
    public class Controller : IController
    {
        private PlanetRepository planets;
        
        //private UnitRepository unitRepository;
        //private WeaponRepository weaponRepository;

        public Controller()
        {
            planets = new PlanetRepository();
            //unitRepository = new UnitRepository();
            //weaponRepository = new WeaponRepository();
        }
        public string CreatePlanet(string name, double budget)
        {
            IPlanet planet = null;
            var existingPlanet = planets.Models.FirstOrDefault(w => w.Name == name);  //??? if(planets.FindByName(name) != null)

            if (existingPlanet != null)
            {
                return string.Format(OutputMessages.ExistingPlanet, name);
            }
            planet = new Planet(name, budget);
            planets.AddItem(planet);


            return string.Format(OutputMessages.NewPlanet, name);   
        }
        public string AddUnit(string unitTypeName, string planetName)
        {
            //IPlanet planet = null;
            //var existingPlanet = planets.Models.FirstOrDefault(w => w.Name == planetName);
            var existingPlanet = planets.FindByName(planetName);
            if (existingPlanet == null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.UnexistingPlanet, planetName));
            }
            if (existingPlanet.Army.Any(u => u.GetType().Name == unitTypeName))
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.UnitAlreadyAdded, unitTypeName,
                    planetName));
            }

            IMilitaryUnit unit = null;
            switch (unitTypeName)
            {
                case nameof(StormTroopers):
                    unit = new StormTroopers();
                    break;
                case nameof(SpaceForces):
                    unit = new SpaceForces();
                    break;
                case nameof(AnonymousImpactUnit):
                    unit = new AnonymousImpactUnit();
                    break;
                default:
                    throw new InvalidOperationException(string.Format(ExceptionMessages.ItemNotAvailable, unitTypeName));
            }

            existingPlanet.Spend(unit.Cost);
            existingPlanet.AddUnit(unit);

            return string.Format(OutputMessages.UnitAdded, unitTypeName, planetName);
        }

        public string AddWeapon(string planetName, string weaponTypeName, int destructionLevel)
        {
            //IPlanet planet = null;
            //var existingPlanet = planets.Models.FirstOrDefault(w => w.Name == planetName);
              var existingPlanet = planets.FindByName(planetName);
            if (existingPlanet == null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.UnexistingPlanet, planetName));
            }
            if (existingPlanet.Weapons.Any(w => w.GetType().Name == weaponTypeName))
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.WeaponAlreadyAdded, weaponTypeName,
                    planetName));
            }

            IWeapon weapon = null;
            switch (weaponTypeName)
            {
                case nameof(BioChemicalWeapon):
                    weapon = new BioChemicalWeapon(destructionLevel);
                    break;
                case nameof(NuclearWeapon):
                    weapon = new NuclearWeapon(destructionLevel);
                    break;
                case nameof(SpaceMissiles):
                    weapon = new SpaceMissiles(destructionLevel);
                    break;
                default:
                    throw new InvalidOperationException(string.Format(ExceptionMessages.ItemNotAvailable, weaponTypeName));
            }

            existingPlanet.Spend(weapon.Price);
            existingPlanet.AddWeapon(weapon);

            return string.Format(OutputMessages.WeaponAdded, planetName, weaponTypeName);
        }       

        public string ForcesReport()
        {
            StringBuilder sb = new StringBuilder();

            var orderedPlanet = planets.Models.OrderBy(h => h.GetType().Name)
                .ThenByDescending(h => h.MilitaryPower)
                .ThenBy(h => h.Name);
            //var ordered = planets.Models.OrderByDescending(p => p.MilitaryPower).ThenBy(p => p.Name);
            sb.AppendLine("***UNIVERSE PLANET MILITARY REPORT***");
            foreach (var item in orderedPlanet)
            {
                sb.AppendLine(item.PlanetInfo());
            }

            return sb.ToString().TrimEnd();
        }
        public string SpecializeForces(string planetName)
        {
            var planet = planets.FindByName(planetName);

            if (planet == null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.UnexistingPlanet, planetName));
            }
            if (!planet.Army.Any())
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.NoUnitsFound));
            }
            planet.Spend(1.25);
            planet.TrainArmy();

            return string.Format(OutputMessages.ForcesUpgraded, planetName);
        }


        public string SpaceCombat(string planetOne, string planetTwo)
        {
            var attacker = planets.FindByName(planetOne);
            var defender = planets.FindByName(planetTwo);

            bool attackerIsNuclear = attacker.Weapons.Any(w => w is NuclearWeapon);
            bool defenderIsNuclear = defender.Weapons.Any(w => w is NuclearWeapon);
            IPlanet winner = null;
            IPlanet looser = null;
            if (attacker.MilitaryPower > defender.MilitaryPower)
            {
                winner = attacker;
                looser = defender;
            }
            else if (defender.MilitaryPower > attacker.MilitaryPower)
            {
                winner = defender;
                looser = attacker;
            }
            else
            {
                if (attackerIsNuclear && !defenderIsNuclear)
                {
                    winner = attacker;
                    looser = defender;
                }
                else if (defenderIsNuclear && !attackerIsNuclear)
                {
                    winner = defender;
                    looser = attacker;
                }
                else
                {
                    attacker.Spend(attacker.Budget / 2);
                    defender.Spend(defender.Budget / 2);

                    return OutputMessages.NoWinner;
                }
            }

            winner.Spend(winner.Budget / 2);
            winner.Profit(looser.Budget / 2);
            winner.Profit(looser.Army.Sum(u => u.Cost) + looser.Weapons.Sum(w => w.Price));

            planets.RemoveItem(looser.Name);

            return string.Format(OutputMessages.WinnigTheWar, winner.Name, looser.Name);
        }       
    }
}
