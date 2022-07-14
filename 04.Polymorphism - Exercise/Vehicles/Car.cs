
namespace Vehicles
{
    public class Car : Vehicle
    {
        public Car(double fuelCuantity, double fuelConsumption) 
            : base(fuelCuantity, fuelConsumption)
        {
        }
        public override double FuelConsumption => base.FuelConsumption + 0.9;
    }
}
