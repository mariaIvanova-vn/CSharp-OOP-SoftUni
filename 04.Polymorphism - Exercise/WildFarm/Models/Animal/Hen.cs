using System;
using System.Collections.Generic;
using System.Text;
using WildFarm.Models.Food;

namespace WildFarm.Models.Animal
{
    internal class Hen : Bird
    {
        private const double Increases = 0.35;
        public Hen(string name, double weight, double wingSize) 
            : base(name, weight, wingSize)
        {
        }

        public override void Eat(IFood food)
        {
            this.Weight += Increases * food.Quantity;
            this.FoodEaten += food.Quantity;
        }

        public override string ProduceSound() => "Cluck";
    }
}
