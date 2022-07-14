
namespace Vehicles
{
    public class Truck : Vehicle
    {
        public Truck(double fuelCuantity, double fuelConsumption) 
            : base(fuelCuantity, fuelConsumption)
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
