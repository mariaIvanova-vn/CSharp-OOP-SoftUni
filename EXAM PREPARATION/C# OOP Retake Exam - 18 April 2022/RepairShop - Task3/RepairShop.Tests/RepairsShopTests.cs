using NUnit.Framework;
using System;

namespace RepairShop.Tests
{
    public class Tests
    {
        public class RepairsShopTests
        {
            [Test]
            public void CarCreatior()
            {
                Car car = new Car("Toyota", 6);

                Assert.AreEqual(car.CarModel, "Toyota");
                Assert.AreEqual(car.NumberOfIssues, 6);
                Assert.AreEqual(false, car.IsFixed);
            }
            [Test]
            public void GarageCreatior()
            {
                Garage garage = new Garage("Richie", 3);

                Assert.AreEqual("Richie", garage.Name);
                Assert.AreEqual(3, garage.MechanicsAvailable);
                Assert.AreEqual(0,garage.CarsInGarage);
            }
            [Test]
            public void GarageThrowWithNullName()
            {
                Assert.Throws<ArgumentNullException>(() =>
                {
                    Garage garage = new Garage(null, 5);
                });
            }
            [Test]
            public void GarageThrowWithEmptyName()
            {
                Assert.Throws<ArgumentNullException>(() =>
                {
                    Garage garage = new Garage("", 5);
                });
            }
            [Test]
            public void GarageThrowWithNegativeCapacity()
            {
                Assert.Throws<ArgumentException>(() =>
                {
                    Garage garage = new Garage("Richie", -5);
                });
            }
            [Test]
            public void GarageThrowWithZeroCapacity()
            {
                Assert.Throws<ArgumentException>(() =>
                {
                    Garage garage = new Garage("Richie", 0);
                });
            }
            [Test]
            public void AddCar()
            {
                Garage garage = new Garage("Richie", 3);
                Car car = new Car("Toyota", 10);
                garage.AddCar(car);
                Assert.AreEqual(1, garage.CarsInGarage);
            }
            [Test]
            public void AddCarThrow()
            {
                Garage garage = new Garage("Richie", 1);
                Car car = new Car("Toyota", 10);
                Car car2 = new Car("Audi", 3);
                garage.AddCar(car);
                Assert.Throws<InvalidOperationException>(() => { garage.AddCar(car2); });
                
            }
            [Test]
            public void RemoveCar()
            {
                Garage garage = new Garage("Richie", 3);
                Car car = new Car("Toyota", 10);
                garage.AddCar(car);
                garage.FixCar("Toyota");
                garage.RemoveFixedCar();
                Assert.AreEqual(0, garage.CarsInGarage);
            }
            [Test]
            public void RemoveCarWithThrow()
            {
                Garage garage = new Garage("a", 1);
                //Car car = new Car("Toyota", 10);
                //garage.AddCar(car);
                Assert.Throws<InvalidOperationException>(() => { garage.RemoveFixedCar(); });
            }
            [Test]
            public void FixCarWithThrow()
            {
                Garage garage = new Garage("a", 1);
                //Car car = new Car("Toyota", 10);
                //garage.AddCar(car);
                Assert.Throws<InvalidOperationException>(() => { garage.FixCar(""); });
            }
            [Test]
            public void GarageReport()
            {
                Garage garage = new Garage("Richie", 3);
                Car car = new Car("Toyota", 10);
                garage.AddCar(car);

                Assert.AreEqual("There are 1 which are not fixed: Toyota.", garage.Report());
            }
            [Test]
            public void FixCar()
            {
                Garage garage = new Garage("Richie", 3);
                Car car = new Car("Toyota", 10);
                garage.AddCar(car);
                garage.FixCar("Toyota");
                garage.RemoveFixedCar();
                Assert.AreEqual(0, car.NumberOfIssues);
            }
        }
    }
}

