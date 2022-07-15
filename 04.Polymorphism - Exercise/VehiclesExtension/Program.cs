using System;

namespace VehiclesExtension
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string[] carInfo = Console.ReadLine().Split();
            string[] truckInfo = Console.ReadLine().Split();
            string[] busInfo = Console.ReadLine().Split();

            double carFuelQuantity = double.Parse(carInfo[1]);
            double carLitersPerKm = double.Parse(carInfo[2]);
            double carTankCapacity = double.Parse(carInfo[3]);


            double truckFuelQuantity = double.Parse(truckInfo[1]);
            double truckLitersPerKm = double.Parse(truckInfo[2]);
            double truckTankCapacity = double.Parse(truckInfo[3]);

            double busFuelQuantity = double.Parse(busInfo[1]);
            double busLitersPerKm = double.Parse(busInfo[2]);
            double busTankCapacity = double.Parse(busInfo[3]);

            Car car = new Car(carTankCapacity, carFuelQuantity, carLitersPerKm);
            Truck truck = new Truck(truckTankCapacity, truckFuelQuantity, truckLitersPerKm);
            Bus bus = new Bus(busTankCapacity, busFuelQuantity, busLitersPerKm);

            int n = int.Parse(Console.ReadLine());
            for (int i = 0; i < n; i++)
            {
                try
                {
                    string[] command = Console.ReadLine().Split();
                    string action = command[0];
                    string vehicle = command[1];
                    double value = double.Parse(command[2]);
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
                        else if (vehicle == "Truck")
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
                        else if (vehicle == "Bus")
                        {
                            if (bus.CanDrive(value))
                            {
                                bus.Drive(value);
                                Console.WriteLine($"Bus travelled {value} km");
                            }
                            else
                            {
                                Console.WriteLine("Bus needs refueling");
                            }
                        }
                    }
                    else if (action == "DriveEmpty")
                    {
                        bus.IsEmpty = true;
                        if (bus.CanDrive(value))
                        {
                            bus.Drive(value);
                            Console.WriteLine($"Bus travelled {value} km");
                        }
                        else
                        {
                            Console.WriteLine("Bus needs refueling");
                        }
                        bus.IsEmpty = false;
                    }
                    else if (action == "Refuel")
                    {
                        if (vehicle == "Car")
                        {
                            if (car.CanRefuel(value))
                            {
                                car.Refuel(value);
                            }
                            else
                            {
                                Console.WriteLine($"Cannot fit {value} fuel in the tank");
                            }
                        }
                        else if (vehicle == "Truck")
                        {
                            if (truck.CanRefuel(value))
                            {
                                truck.Refuel(value);
                            }
                            else
                            {
                                Console.WriteLine($"Cannot fit {value} fuel in the tank");
                            }
                        }
                        else if (vehicle == "Bus")
                        {
                            if (bus.CanRefuel(value))
                            {
                                bus.Refuel(value);
                            }
                            else
                            {
                                Console.WriteLine($"Cannot fit {value} fuel in the tank");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message);
                }
            }
            Console.WriteLine($"Car: {car.FuelCuantity:f2}");
            Console.WriteLine($"Truck: {truck.FuelCuantity:f2}");
            Console.WriteLine($"Bus: {bus.FuelCuantity:f2}");
        }
    }
}

