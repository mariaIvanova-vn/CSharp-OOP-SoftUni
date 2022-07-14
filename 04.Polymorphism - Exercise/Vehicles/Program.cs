using System;

namespace Vehicles
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string[] carInfo = Console.ReadLine().Split();
            string[] truckInfo = Console.ReadLine().Split();

            double carFuelQuantity = double.Parse(carInfo[1]);
            double carLitersPerKm = double.Parse(carInfo[2]);

            double truckFuelQuantity = double.Parse(truckInfo[1]);
            double truckLitersPerKm = double.Parse(truckInfo[2]);

            Car car = new Car(carFuelQuantity, carLitersPerKm);
            Truck truck = new Truck(truckFuelQuantity, truckLitersPerKm);

            int n = int.Parse(Console.ReadLine());
            for (int i = 0; i < n; i++)
            {
                string[] command = Console.ReadLine().Split();
                string action = command[0];
                string vehicle = command[1];
                double value=double.Parse(command[2]);
                if (action == "Drive")
                {
                    if (vehicle == "Car")
                    {
                        if (car.CanDrive(value))
                        {
                            car.Drive(value);
                            Console.WriteLine($"Car travelled {value} km");
                        }
                        else
                        {
                            Console.WriteLine("Car needs refueling");
                        }
                    }
                    else
                    {
                        if (truck.CanDrive(value))
                        {
                            truck.Drive(value);
                            Console.WriteLine($"Truck travelled {value} km");
                        }
                        else
                        {
                            Console.WriteLine("Truck needs refueling");
                        }
                    }
                }
                else
                {
                    if (vehicle == "Car")
                    {
                        car.Refuel(value);
                    }
                    else
                    {
                        truck.Refuel(value);
                    }
                }
            }
            Console.WriteLine($"Car: {car.FuelCuantity:f2}");
            Console.WriteLine($"Truck: {truck.FuelCuantity:f2}");
        }
    }
}

