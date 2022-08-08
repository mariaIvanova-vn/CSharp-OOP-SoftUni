using AquaShop.Core.Contracts;
using AquaShop.Models.Aquariums;
using AquaShop.Models.Aquariums.Contracts;
using AquaShop.Models.Decorations;
using AquaShop.Models.Decorations.Contracts;
using AquaShop.Models.Fish;
using AquaShop.Models.Fish.Contracts;
using AquaShop.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AquaShop.Core
{
    public class Controller : IController
    {
        private readonly DecorationRepository decorations;
        private readonly ICollection<IAquarium> aquariums;

        public Controller()
        {
            decorations = new DecorationRepository();   
            aquariums = new List<IAquarium>();
        }
        public string AddAquarium(string aquariumType, string aquariumName)
        {
            IAquarium aquarium;
            if (aquariumType == "FreshwaterAquarium")
            {
                aquarium = new FreshwaterAquarium(aquariumName);
            }
            else if (aquariumType == "SaltwaterAquarium")
            {
                aquarium = new SaltwaterAquarium(aquariumName);
            }
            else
            {
                throw new InvalidOperationException("Invalid aquarium type.");
            }
            aquariums.Add(aquarium);
            return $"Successfully added {aquariumType}.";
        }

        public string AddDecoration(string decorationType)
        {
            IDecoration decoration;
            if (decorationType == "Ornament")
            {
                decoration = new Ornament();
            }
            else if (decorationType == "Plant")
            {
                decoration = new Plant();
            }
            else
            {
                throw new InvalidOperationException("Invalid decoration type.");
            }
            decorations.Add(decoration);
            return $"Successfully added {decorationType}.";
        }

        public string AddFish(string aquariumName, string fishType, string fishName, string fishSpecies, decimal price)
        {
            var searchAquarium = aquariums.FirstOrDefault(a => a.Name == aquariumName);
            IFish fish;
            if (fishType == "FreshwaterFish")
            {
                fish = new FreshwaterFish(fishName,fishSpecies,price);
            }
            else if (fishType == "SaltwaterFish")
            {
                fish = new SaltwaterFish(fishName, fishSpecies, price);
            }
            else
            {
                throw new InvalidOperationException("Invalid fish type.");
            }
            if ((fishType == nameof(FreshwaterFish) && searchAquarium.GetType().Name == nameof(SaltwaterAquarium)) ||
                (fishType == nameof(SaltwaterFish) && searchAquarium.GetType().Name == nameof(FreshwaterAquarium)))
            {
                return "Water not suitable.";
            }
            searchAquarium.AddFish(fish);
            return $"Successfully added {fishType} to {aquariumName}.";
        }

        public string CalculateValue(string aquariumName)
        {
            var searchAquarium = aquariums.FirstOrDefault(a => a.Name == aquariumName);
            var totalValue = searchAquarium.Fish.Sum((f => f.Price)) + searchAquarium.Decorations.Sum(d => d.Price);
            return $"The value of Aquarium {aquariumName} is {totalValue:f2}.";
        }

        public string FeedFish(string aquariumName)
        {
            var searchAquarium = aquariums.FirstOrDefault(a => a.Name == aquariumName);
            searchAquarium.Feed();

            return $"Fish fed: {searchAquarium.Fish.Count}";
        }

        public string InsertDecoration(string aquariumName, string decorationType)
        {
          var desiredDecoration = decorations.FindByType(decorationType);
            var searchAquarium = aquariums.FirstOrDefault(a => a.Name == aquariumName);
            if (desiredDecoration == null)
            {
                throw new InvalidOperationException($"There isn't a decoration of type {decorationType}.");
            }
            searchAquarium?.AddDecoration(desiredDecoration);
            decorations.Remove(desiredDecoration);

            return $"Successfully added {decorationType} to {aquariumName}.";
        }

        public string Report()
        {
            StringBuilder stringBuilder = new StringBuilder();

            foreach (var item in aquariums)
            {
                stringBuilder.AppendLine(item.GetInfo());
            }
            return stringBuilder.ToString().TrimEnd();
        }
    }
}
