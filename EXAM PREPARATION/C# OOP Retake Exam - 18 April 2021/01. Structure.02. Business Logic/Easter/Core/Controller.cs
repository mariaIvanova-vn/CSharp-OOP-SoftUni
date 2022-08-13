using Easter.Core.Contracts;
using Easter.Models.Bunnies;
using Easter.Models.Bunnies.Contracts;
using Easter.Models.Dyes;
using Easter.Models.Dyes.Contracts;
using Easter.Models.Eggs;
using Easter.Repositories;
using Easter.Utilities.Messages;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Easter.Models.Eggs.Contracts;
using Easter.Models.Workshops;

namespace Easter.Core
{
    public class Controller : IController
    {
        private BunnyRepository bunnies;
        private EggRepository eggs;

        public Controller()
        {
            bunnies = new BunnyRepository();
            eggs = new EggRepository(); 
        }

        public string AddBunny(string bunnyType, string bunnyName)
        {
            IBunny bunny = null;
            if (bunnyType == "HappyBunny")
            {
                bunny = new HappyBunny(bunnyName);
            }
            else if (bunnyType == "SleepyBunny")
            {
                bunny = new SleepyBunny(bunnyName);
            }
            else
            {
                throw new InvalidOperationException(ExceptionMessages.InvalidBunnyType);
            }
            bunnies.Add(bunny);
            return string.Format(OutputMessages.BunnyAdded, bunnyType, bunnyName);
        }

        public string AddDyeToBunny(string bunnyName, int power)
        {
            Dye dye = new Dye(power);
            var bunny = bunnies.FindByName(bunnyName);
            if (bunny == null)
            {
                throw new InvalidOperationException(ExceptionMessages.InexistentBunny);
            }
            bunny.AddDye(dye);
            bunnies.Add(bunny);

            return string.Format(OutputMessages.DyeAdded, power, bunnyName);
        }

        public string AddEgg(string eggName, int energyRequired)
        {
            Egg egg = new Egg(eggName, energyRequired);

            eggs.Add(egg);  

            return string.Format(OutputMessages.EggAdded, eggName);
        }

        public string ColorEgg(string eggName)
        {
            var bunny = bunnies.Models.Where(b => b.Energy >= 50).OrderByDescending(b => b.Energy);
            if (!bunny.Any())
            {
                throw new InvalidOperationException(ExceptionMessages.BunniesNotReady);
            }

            IEgg eggToColor = eggs.FindByName(eggName);
            Workshop workshop = new Workshop();

            foreach (var item in bunny)
            {
                workshop.Color(eggToColor, item);

                if (item.Energy == 0)
                {
                    bunnies.Remove(item);
                }
                if (eggToColor.IsDone() == true)
                {
                    break;
                }
            }
            if (eggToColor.IsDone() == true)
            {
                return string.Format(OutputMessages.EggIsDone, eggName);
            }
            else
            {
                return string.Format(OutputMessages.EggIsNotDone, eggName);
            }            
        }

        public string Report()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"{eggs.Models.Count(e => e.IsDone() == true)} eggs are done!");
            sb.AppendLine("Bunnies info:");

            foreach (var model in bunnies.Models)
            {
                int countDyesNotFinished = model.Dyes.Count(d => d.IsFinished() == false);

                sb.AppendLine($"Name: {model.Name}");
                sb.AppendLine($"Energy: {model.Energy}");
                sb.AppendLine($"Dyes: {countDyesNotFinished} not finished");
            }

            return sb.ToString().TrimEnd();
        }
    }
}