namespace Aquariums.Tests
{
    using NUnit.Framework;
    using System;

    public class AquariumsTests
    {
        //[Test]
        //public void AquariumCreatior()
        //{
        //    Aquarium aquarium = new Aquarium("Richie", 3);

        //    Assert.AreEqual("Richie", aquarium.Name);
        //    Assert.AreEqual(0, aquarium.Count);
        //    Assert.AreEqual(3, aquarium.Capacity);
        //}
        [Test]
        public void FishCreatior()
        {
            Fish fish = new Fish("Ribcho");

            Assert.AreEqual(fish.Name, "Ribcho");
            Assert.IsTrue(fish.Available);
        }     
        [Test]
        public void AquariumThrowWithNullAndEmptyName()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                Aquarium aquarium = new Aquarium(null, 5);
            });

            Assert.Throws<ArgumentNullException>(() =>
            {
                Aquarium aquarium = new Aquarium(string.Empty, 5);
            });
        }
        //}
        [Test]
        public void AquariumNamePropertyWorkCorrectly()
        {
            Aquarium aquarium = new Aquarium("Richie", 3);

            Assert.That(aquarium.Name, Is.EqualTo("Richie"));
        }
        [Test]
        public void AquariumThrowWithNegativeCapacity()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                Aquarium aquarium = new Aquarium("fish", -1);
            });
        }
        [Test]
        public void AquariumWithCorrectCapacity()
        {
            Aquarium aquarium = new Aquarium("Richie", 3);

            Assert.That(aquarium.Capacity , Is.EqualTo(3)); 
        }
        [Test]
        public void AddFishThrow()
        {
            Aquarium aquarium = new Aquarium("Richie", 1);
            Fish fish = new Fish("Fishi");
            Fish fish2 = new Fish("Fishko");
            aquarium.Add(fish);
            Assert.Throws<InvalidOperationException>(() =>
            {
                aquarium.Add(fish2);
                Assert.That(aquarium.Capacity, Is.EqualTo(aquarium.Count));
            });
        }
        [Test]
        public void AddFish()
        {
            Aquarium aquarium = new Aquarium("Richie", 3);
            Fish fish = new Fish("Fishi");
            aquarium.Add(fish);
            Assert.That(aquarium.Count, Is.EqualTo(1));
        }
        [Test]
        public void RemoveFish()
        {
            Aquarium aquarium = new Aquarium("Richie", 3);
            Fish fish = new Fish("Fishi");
            aquarium.Add(fish);
            aquarium.RemoveFish("Fishi");
            Assert.AreEqual(0, aquarium.Count);
        }
        [Test]
        public void RemoveFishThrow()
        {
            Aquarium aquarium = new Aquarium("Richie", 3);
            Fish fish = new Fish("Fishi");
            Fish fish2 = new Fish("Fishko");
            aquarium.Add(fish);
            aquarium.RemoveFish("Fishi");
            Assert.Throws<InvalidOperationException>(() =>
            {
                aquarium.RemoveFish("fff");
            });
        }
        [Test]
        public void AquariumReport()
        {
            Aquarium aquarium = new Aquarium("Richie", 3);
            Fish fish = new Fish("Fishi");
            aquarium.Add(fish);

            Assert.AreEqual("Fish available at Richie: Fishi", aquarium.Report());
        }
        [Test]
        public void SellFishThrow()
        {
            Aquarium aquarium = new Aquarium("Richie", 3);

            Assert.Throws<InvalidOperationException>(() => { Fish sellFish = aquarium.SellFish(null); });
        }
        [Test]
        public void SellFish()
        {
            Aquarium aquarium = new Aquarium("Richie", 3);
            Fish fish = new Fish("Fishi");
            aquarium.Add(fish);
            Fish sellFish =  aquarium.SellFish("Fishi");
            Assert.IsFalse(sellFish.Available);
        }
    }
}
