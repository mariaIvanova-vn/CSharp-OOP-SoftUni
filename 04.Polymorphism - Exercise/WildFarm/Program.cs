
using System;
using System.Collections.Generic;
using WildFarm.Models.Animal;
using WildFarm.Models.Food;

namespace WildFarm
{
    public class Program
    {
        static void Main(string[] args)
        {
            List<IAnimal> animals = new List<IAnimal>();
            string input;
            while ((input = Console.ReadLine()) != "End")
            {
                try
                {
                    string[] animalInfo = input.Split();
                    string[] foodInfo = Console.ReadLine().Split();

                    string type = animalInfo[0];
                    string name = animalInfo[1];
                    double weigth = double.Parse(animalInfo[2]);

                    IAnimal animal = null;
                    if (type == "Cat" || type == "Tiger")
                    {     // "{AnimalType} [{AnimalName}, {Breed}, {AnimalWeight}, {AnimalLivingRegion}, {FoodEaten}]
                          //Cat Sammy 1.1 Home Persian
                        string livingRegion = animalInfo[3];
                        string breed = animalInfo[4];
                        if (type == "Cat")
                        {
                            animal = new Cat(name, weigth, livingRegion, breed);
                        }
                        else
                        {
                            animal = new Tiger(name, weigth, livingRegion, breed);
                        }
                    }
                    else if (type == "Hen" || type == "Owl")
                    {
                        double wingSize = double.Parse(animalInfo[3]);
                        if (type == "Hen")
                        {
                            animal = new Hen(name, weigth, wingSize);
                        }
                        else
                        {
                            animal = new Owl(name, weigth, wingSize);
                        }
                    }
                    else if (type == "Dog" || type == "Mouse")
                    {
                        string livingRegion = animalInfo[3];
                        if (type == "Dog")
                        {
                            animal = new Dog(name, weigth, livingRegion);
                        }
                        else
                        {
                            animal = new Mouse(name, weigth, livingRegion);
                        }
                    }
                    Console.WriteLine(animal.ProduceSound());
                    animals.Add(animal);

                    IFood food = null;
                    string foodType = foodInfo[0];
                    int qty = int.Parse(foodInfo[1]);
                    if (foodType == "Vegetable")
                    {
                        food = new Vegetable(qty);
                    }
                    else if (foodType == "Fruit")
                    {
                        food = new Fruit(qty);
                    }
                    else if (foodType == "Meat")
                    {
                        food = new Meat(qty);
                    }
                    else
                    {
                        food = new Seeds(qty);
                    }
                    animal.Eat(food);
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message);
                }
            }
            foreach (var item in animals)
            {
                Console.WriteLine(item.ToString());
            }
        }
    }
}
