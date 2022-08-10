using NUnit.Framework;
using System;

namespace SmartphoneShop.Tests
{
    [TestFixture]
    public class SmartphoneShopTests
    {
        [Test]
        public void ShopThrowWithNegativeCapacity()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                Shop shop = new Shop(-5);
            });
        }
        [Test]
        public void ShopCapacity()
        {
            Shop shop = new Shop(5);
            Assert.AreEqual(5, shop.Capacity);
            Assert.AreEqual(0, shop.Count);
        }
        [Test]
        public void AddPhoneThrow()
        {
            Shop shop = new Shop(1);
            Smartphone smartphone = new Smartphone("Samsung", 100);
            Smartphone smartphone1 = new Smartphone("Nokia", 67);
            shop.Add(smartphone);
            Assert.Throws<InvalidOperationException>(() => { shop.Add(smartphone1); });
            
            Assert.Throws<InvalidOperationException>(() => { shop.Add(smartphone); });
        }
        [Test]
        public void AddPhoneThrowName()
        {
            Shop shop = new Shop(15);
            Smartphone smartphone = new Smartphone("Samsung", 100);
            Smartphone smartphone1 = new Smartphone("Nokia", 67);
            shop.Add(smartphone);

            Assert.Throws<InvalidOperationException>(() => { shop.Add(smartphone); });
        }
        [Test]
        public void AddPhone()
        {
            Shop shop = new Shop(4);
            Smartphone smartphone = new Smartphone("Samsung", 100);
            Smartphone smartphone1 = new Smartphone("Nokia", 67);
            shop.Add(smartphone);
            shop.Add(smartphone1);
            Assert.That(2, Is.EqualTo(shop.Count));
            Assert.That(4, Is.EqualTo(shop.Capacity));
        }
        [Test]
        public void RemovePhone()
        {
            Shop shop = new Shop(4);
            Smartphone smartphone = new Smartphone("Samsung", 100);
            shop.Add(smartphone);
            shop.Remove("Samsung");
            Assert.AreEqual(0, shop.Count);
        }
        [Test]
        public void RemovePhoneWithThrow()
        {
            Shop shop = new Shop(4);

            Assert.Throws<InvalidOperationException>(() => { shop.Remove("Samsung"); });
        }
        [Test]
        public void TestPhone()
        {
            Shop shop = new Shop(4);
            Smartphone smartphone = new Smartphone("Samsung", 10);
            shop.Add(smartphone);
            smartphone.CurrentBateryCharge = 50;

            Assert.Throws<InvalidOperationException>(() => { shop.TestPhone(null, 37); });
            Assert.Throws<InvalidOperationException>(() => { shop.TestPhone("Samsung555", 1); });
        }
        [Test]
        public void TestPhoneMethodShouldThrowExceptionIfBatteryChargeIsLessThanBatteryUsage()
        {
            Shop shop = new Shop(4);
            Smartphone smartphone = new Smartphone("Samsung", 100);
            shop.Add(smartphone);
            shop.TestPhone("Samsung", 40);
            shop.TestPhone("Samsung", 40);

            Assert.That(() => shop.TestPhone("Samsung", 40), Throws.InvalidOperationException);
        }
        [Test]
        [TestCase(40)]
        [TestCase(100)]
        public void TestMethodShouldReducePhoneCurrentBatteryCharge(int batteryUsage)
        {
            Shop shop = new Shop(4);
            Smartphone smartphone = new Smartphone("Samsung", 100);
            shop.Add(smartphone);
            int initialBatteryCharge = smartphone.CurrentBateryCharge;

            shop.TestPhone("Samsung", batteryUsage);

            Assert.AreEqual(initialBatteryCharge - batteryUsage, smartphone.CurrentBateryCharge);
        }
        [Test]
        public void ChargePhone()
        {
            Shop shop = new Shop(4);
            Smartphone smartphone = new Smartphone("Samsung", 100);
            Smartphone smartphone1 = new Smartphone("Nokia", 67);
            shop.Add(smartphone);
            smartphone.CurrentBateryCharge = 100;
            Assert.AreEqual(smartphone.CurrentBateryCharge, 100);
            Assert.AreEqual(smartphone.CurrentBateryCharge, smartphone.MaximumBatteryCharge);
            
            Assert.Throws<InvalidOperationException>(() => { shop.ChargePhone(null); });
            Assert.Throws<InvalidOperationException>(() => { shop.ChargePhone("Nokia1"); });
        }
        [Test]
        public void ChargePhoneMethodShouldChargePhoneToMaximumCharge()
        {
            Shop shop = new Shop(4);
            Smartphone smartphone = new Smartphone("Samsung", 100);

            shop.Add(smartphone);
            shop.TestPhone("Samsung", 40);

            Assert.AreEqual(60, smartphone.CurrentBateryCharge);

            shop.ChargePhone("Samsung");

            Assert.AreEqual(100, smartphone.CurrentBateryCharge);
        }
    }
}