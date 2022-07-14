using System;

namespace Vehicles
{
    public abstract class Vehicle
    {
        protected Vehicle(double fuelCuantity, double fuelConsumption)
        {
            this.FuelCuantity = fuelCuantity;
            this.FuelConsumption = fuelConsumption;
        }

        public double FuelCuantity { get; set; }
        public virtual double FuelConsumption { get; set; }

        public bool CanDrive(double km) => this.FuelCuantity - (km * this.FuelConsumption) >= 0;

        public void Drive(double km)
        {
            if (CanDrive(km))
            {
                this.FuelCuantity -= (km * this.FuelConsumption);
            }
        }
        public virtual void Refuel(double amount)
        {
            this.FuelCuantity += amount;
        }
    }
}
