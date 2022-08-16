using NUnit.Framework;
using System;

namespace PlanetWars.Tests
{
    public class Tests
    {
        [Test]
        public void WeaponCreatior()
        {
            Weapon weapon = new Weapon("AAAA", 200, 10);

            Assert.AreEqual(weapon.Name, "AAAA");
            Assert.AreEqual(weapon.DestructionLevel, 10);
            Assert.AreEqual(weapon.IsNuclear, true);
            Assert.AreEqual(weapon.Price, 200);

        }
        [Test]
        public void WeaponCreatiorTrowWithNegativePrice()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                Weapon weapon = new Weapon("AAAA", -1, 10);
            });
        }
        [Test]
        public void PlanetCreatior()
        {
            Planet planet = new Planet("Neptun", 100);

            Assert.AreEqual(planet.Name, "Neptun");
            Assert.AreEqual(planet.Budget, 100);
            Assert.That(planet, Is.Not.Null);
            Assert.AreEqual(0, planet.Weapons.Count);
        }
        [Test]
        public void PlanetThrowWithNullName()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                Planet planet = new Planet(null, 100);
            });
        }
        [Test]
        public void PlanetThrowWithEmptyName()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                Planet planet = new Planet("", 100);
            });
        }
        [Test]
        public void PlanetThrowWithNegativeBudget()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                Planet planet = new Planet("Neptun", -1);
            });
        }
        [Test]
        public void MilitaryPowerRatioShouldReturnCorrectSum()
        {
            Planet planet = new Planet("Mars", 100.5);
            planet.AddWeapon(new Weapon("Gun", 15.8, 50));
            planet.AddWeapon(new Weapon("Laser", 117.2, 60));

            Assert.AreEqual(110, planet.MilitaryPowerRatio);
        }
        [Test]
        public void AddWeapon()
        {
            Planet planet = new Planet("Neptun", 100);
            Weapon weapon = new Weapon("AAAA", 200, 10);
            planet.AddWeapon(weapon);

            Assert.AreEqual(planet.Weapons.Count, 1);
        }
        [Test]
        public void AddWeaponThrow()
        {
            Planet planet = new Planet("Neptun", 100);
            Weapon weapon = new Weapon("AAAA", 200, 10);
            planet.AddWeapon(weapon);
            Assert.Throws<InvalidOperationException>(() => { planet.AddWeapon(weapon); });
        }
        [Test]
        public void RemoveWeapon()
        {
            Planet planet = new Planet("Neptun", 100);
            Weapon weapon = new Weapon("AAAA", 200, 10);
            planet.AddWeapon(weapon);
            planet.RemoveWeapon("AAAA");
            Assert.AreEqual(planet.Weapons.Count, 0);
        }
        [Test]
        public void WeaponMilitaryPowerRatio()
        {
            Planet planet = new Planet("Neptun", 100);
            Weapon weapon = new Weapon("AAAA", 200, 10);
            Weapon weapon1 = new Weapon("CCC", 5, 1);
            planet.AddWeapon(weapon);
            planet.AddWeapon(weapon1);

            Assert.AreEqual(planet.MilitaryPowerRatio, 11);
        }
        [Test]
        public void WeaponProfit()
        {
            Planet planet = new Planet("Neptun", 100);
            Weapon weapon = new Weapon("AAAA", 200, 10);
            Weapon weapon1 = new Weapon("CCC", 5, 1);
            planet.AddWeapon(weapon);
            planet.AddWeapon(weapon1);

            planet.Profit(5);

            Assert.AreEqual(planet.Budget, 105);
        }
        [Test]
        public void WeaponProfitSpendFunds()
        {
            Planet planet = new Planet("Neptun", 100);
            Weapon weapon = new Weapon("AAAA", 200, 10);
            Weapon weapon1 = new Weapon("CCC", 5, 1);
            planet.AddWeapon(weapon);
            planet.AddWeapon(weapon1);

            planet.SpendFunds(5);

            Assert.AreEqual(planet.Budget, 95);
        }
        [Test]
        public void WeaponProfitSpendFundsThrow()
        {
            Planet planet = new Planet("Neptun", 100);
            Weapon weapon = new Weapon("AAAA", 200, 10);
            Weapon weapon1 = new Weapon("CCC", 5, 1);
            planet.AddWeapon(weapon);
            planet.AddWeapon(weapon1);

            Assert.Throws<InvalidOperationException>(() => { planet.SpendFunds(299); });
        }
        [Test]
        public void UpgradeWeaponThrow()
        {
            Planet planet = new Planet("Neptun", 100);
            Weapon weapon = new Weapon("AAAA", 200, 10);
            Weapon weapon1 = new Weapon("CCC", 5, 1);
            planet.AddWeapon(weapon);
            planet.AddWeapon(weapon1);

            Assert.Throws<InvalidOperationException>(() => { planet.UpgradeWeapon("Richie"); });
        }
        [Test]
        public void UpgradeWeapon()
        {
            Planet planet = new Planet("Neptun", 100);
            Weapon weapon = new Weapon("AAAA", 200, 10);
            Weapon weapon1 = new Weapon("CCC", 5, 1);
            planet.AddWeapon(weapon);
            planet.AddWeapon(weapon1);

            planet.UpgradeWeapon("AAAA");
            Assert.AreEqual(11, weapon.DestructionLevel);
        }
        [Test]
        public void DestructOpponent()
        {
            Planet planet = new Planet("Neptun", 100);
            Planet planet1 = new Planet("Pluton", 40);
            Weapon weapon = new Weapon("AAAA", 200, 10);
            Weapon weapon1 = new Weapon("CCC", 5, 1);
            planet.AddWeapon(weapon);
            planet.AddWeapon(weapon1);

            var result =  planet.DestructOpponent(planet1);

            Assert.IsNotNull(result);
            Assert.AreEqual(result, "Pluton is destructed!");
        }
        [Test]
        public void DestructOpponentThrow()
        {
            Planet planet = new Planet("Neptun", 100);
            Planet planet1 = new Planet("Pluton", 300);
            Weapon weapon = new Weapon("AAAA", 200, 10);
            Weapon weapon1 = new Weapon("CCC", 5, 1);
            planet.AddWeapon(weapon);
            planet.AddWeapon(weapon1);

            Assert.Throws<NullReferenceException>(() => { planet.DestructOpponent(null); });
        }
        [Test]
        public void SpendFunds()
        {
            Planet planet = new Planet("Neptun", 100);
            Planet planet1 = new Planet("Pluton", 40);
            Weapon weapon = new Weapon("AAAA", 200, 10);
            Weapon weapon1 = new Weapon("CCC", 5, 1);
            planet.AddWeapon(weapon);
            planet.AddWeapon(weapon1);

            planet.SpendFunds(10);            
            Assert.AreEqual(90, planet.Budget);

            planet.Profit(5);
            Assert.AreEqual(95, planet.Budget);
        }
        [Test]
        [TestCase(50, 60)]
        [TestCase(51, 60)]

        public void DestructOpponentShouldThrowExceptionIfNotEnoughPower(int weapon1, int weapon2)
        {
            Planet attacker = new Planet("Mars", 100.5);
            Planet defender = new Planet("Earth", 200.3);

            attacker.AddWeapon(new Weapon("Gun", 15.8, 50));
            attacker.AddWeapon(new Weapon("Laser", 117.2, 60));

            defender.AddWeapon(new Weapon("Laser", 117.2, weapon1));
            defender.AddWeapon(new Weapon("WaterGun", 117.2, weapon2));

            Assert.Throws<InvalidOperationException>(() => attacker.DestructOpponent(defender));
        }
    }
}
