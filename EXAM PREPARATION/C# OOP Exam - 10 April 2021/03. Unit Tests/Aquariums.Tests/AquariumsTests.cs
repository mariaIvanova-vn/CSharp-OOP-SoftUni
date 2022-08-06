namespace Aquariums.Tests
{
    using NUnit.Framework;
    using System;

    public class AquariumsTests
    {
        [Test]
        public void AquariumCreatior()
        {
            Aquarium aquarium = new Aquarium("Richie", 3);

            Assert.AreEqual("Richie", aquarium.Name);
            Assert.AreEqual(0, aquarium.Count);
            Assert.AreEqual(3, aquarium.Capacity);
        }
        [Test]
        public void AquariumThrowWithNullName()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                Aquarium aquarium = new Aquarium(null, 5);
            });
        }
        [Test]
        public void AquariumThrowWithEmptyName()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                Aquarium aquarium = new Aquarium("", 5);
            });
        }
        [Test]
        public void AquariumThrowWithNegativeCapacity()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                Aquarium aquarium = new Aquarium("fish", -5);
            });
        }
        [Test]
        public void AddFish()
        {
            Aquarium aquarium = new Aquarium("Richie", 3);
            Fish fish = new Fish("Fishi");
            aquarium.Add(fish);
            Assert.AreEqual(1, aquarium.Count);
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
            });           
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
        //[Test]
        //public void SellFish()
        //{
        //    Aquarium aquarium = new Aquarium("Richie", 3);
        //    Fish fish = new Fish("Fishi");
        //    aquarium.Add(fish);
        //    aquarium.SellFish("Fishi");

        //    Assert.AreEqual(false, aquarium.SellFish("Fishi"));
        //}
        [Test]
        public void SellFishThrow()
        {
            Aquarium aquarium = new Aquarium("Richie", 3);           

            Assert.Throws<InvalidOperationException>(() => { aquarium.SellFish(null); });
        }
    }
}
