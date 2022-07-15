
namespace VehiclesExtension
{
    public class Bus : Vehicle
    {
        public Bus(double tankCapacity, double fuelCuantity, double fuelConsumption) 
            : base(tankCapacity, fuelCuantity, fuelConsumption)
        {
        }
        public override double FuelConsumption
            => this.IsEmpty 
            ? base.FuelConsumption
            : base.FuelConsumption + 1.4;
    }
}
