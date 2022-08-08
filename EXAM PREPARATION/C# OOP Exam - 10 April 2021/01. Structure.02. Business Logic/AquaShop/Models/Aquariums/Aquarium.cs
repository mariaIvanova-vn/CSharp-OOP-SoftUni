using AquaShop.Models.Aquariums.Contracts;
using AquaShop.Models.Decorations.Contracts;
using AquaShop.Models.Fish.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AquaShop.Models.Aquariums
{
    public abstract class Aquarium : IAquarium
    {
        private string name;

        private Aquarium()
        {
            this.Decorations = new List<IDecoration>();
            this.Fish = new List<IFish>();
        }
        protected Aquarium(string name, int capacity) 
            :this()
        {
            this.Name = name;   
            this.Capacity = capacity;   
        }
        public string Name
        {
            get => name;
           private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Aquarium name cannot be null or empty.");
                }
                name = value;
            }
        }

        public int Capacity { get; private set; }

        public int Comfort => Decorations.Sum(d => d.Comfort);

        public ICollection<IDecoration> Decorations { get; }

        public ICollection<IFish> Fish { get; }

        public void AddDecoration(IDecoration decoration)
        {
            this.Decorations.Add(decoration);
        }

        public void AddFish(IFish fish)
        {
            if (Capacity == this.Fish.Count)
            {
                throw new InvalidOperationException("Not enough capacity.");
            }
            this.Fish.Add(fish);
        }

        public void Feed()
        {
            foreach (var item in this.Fish)
            {
                item.Eat();
            }
        }

        public bool RemoveFish(IFish fish)
        {
            return this.Fish.Remove(fish);
        }

        public string GetInfo()
        {
            StringBuilder stringBuilder = new StringBuilder();

            string fishName = Fish.Count>0
                ? String.Join(", ", Fish.Select(f=>f.Name))
                : "none";

                stringBuilder.AppendLine($"{this.Name} ({this.GetType().Name}):")
                             .AppendLine($"Fish: {fishName}")
                             .AppendLine($"Decorations: {Decorations.Count}")
                             .AppendLine($"Comfort: {Comfort}");

            return stringBuilder.ToString().TrimEnd();
           
        }
    }
}
