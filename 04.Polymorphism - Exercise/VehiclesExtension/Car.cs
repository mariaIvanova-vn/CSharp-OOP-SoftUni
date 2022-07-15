
namespace VehiclesExtension
{
    public class Car : Vehicle
    {
        public Car(double tankCapacity, double fuelCuantity, double fuelConsumption) 
            : base(tankCapacity, fuelCuantity, fuelConsumption)
        {
        }

        public override double FuelConsumption => base.FuelConsumption + 0.9;
    }
}
