
namespace VehiclesExtension
{
    public class Truck : Vehicle
    {
        public Truck(double tankCapacity, double fuelCuantity, double fuelConsumption) 
            : base(tankCapacity, fuelCuantity, fuelConsumption)
        {
        }

        public override double FuelConsumption => base.FuelConsumption + 1.6;

        public override void Refuel(double amount)
        {
            amount *= 0.95;
            base.Refuel(amount);
        }
    }
}
