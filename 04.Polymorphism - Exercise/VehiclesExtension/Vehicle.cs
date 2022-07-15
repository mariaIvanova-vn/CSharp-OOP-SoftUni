using System;

namespace VehiclesExtension
{
    public abstract class Vehicle
    {
        private double fuelCuantity;

        protected Vehicle(double tankCapacity, double fuelCuantity, double fuelConsumption)
        {
            this.TankCapacity = tankCapacity;
            this.FuelCuantity = fuelCuantity;
            this.FuelConsumption = fuelConsumption;
        }

        public double FuelCuantity 
        {
            get => fuelCuantity;
            private set
            {
                if (value>this.TankCapacity)
                {
                    fuelCuantity = 0;
                }
                else
                {
                    fuelCuantity = value;
                }
            }
        }

        public virtual double FuelConsumption { get; protected set; }
        public double TankCapacity { get; }

        public bool CanDrive(double km) => this.FuelCuantity - (km * this.FuelConsumption) >= 0;

        public bool CanRefuel(double amount) => this.FuelCuantity + amount <= this.TankCapacity;
        public bool IsEmpty { get; set; }
        public void Drive(double km)
        {
            if (CanDrive(km))
            {
                this.FuelCuantity -= (km * this.FuelConsumption);
            }
        }
        public virtual void Refuel(double amount)
        {
            if (amount<=0)
            {
                throw new ArgumentException ("Fuel must be a positive number");
            }
            if (CanRefuel(amount))
            {
                this.FuelCuantity += amount;
            }
        }
    }
}
