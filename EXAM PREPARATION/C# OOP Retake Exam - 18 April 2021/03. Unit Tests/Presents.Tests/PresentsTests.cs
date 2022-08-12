namespace Presents.Tests
{
    using NUnit.Framework;
    using System;
    using System.Reflection;

    [TestFixture]
    public class PresentsTests
    {
        [Test]
        public void PresentCreatior()
        {
            Present present = new Present("doll", 3);
          
            Assert.IsNotNull(present);
            Assert.AreEqual("doll", present.Name);
            Assert.AreEqual(3, present.Magic);
        }
        [Test]
        public void BagCreatior()
        {
            Bag bag = new Bag();
            Present present = new Present("doll", 3);
            
            var result = bag.Create(present);
            Assert.AreEqual("Successfully added present doll.", result);
        }
        [Test]
        public void BagCreatiorThrowWhithNullPresent()
        {
            Bag bag = new Bag();
            Present present = null;

            Assert.Throws<ArgumentNullException>(() => bag.Create(present));
        }
        [Test]
        public void BagCreatiorThrow()
        {
            Bag bag = new Bag();
            Present present = new Present("doll", 3);
            bag.Create(present);
            Assert.Throws<InvalidOperationException>(() => bag.Create(present));
        }
        [Test]
        public void RemovePresent()
        {
            Bag bag = new Bag();
            Present present = new Present("doll", 3);
            Present present1 = new Present("truck", 5);
            bag.Create(present);
            bag.Create(present1);
            bag.Remove(present);
            
            Assert.AreEqual("doll", present.Name);
        }
        [Test]
        public void GetPresentWithLeastMagic()
        {
            Bag bag = new Bag();
            Present present = new Present("doll", 3);
            Present present1 = new Present("truck", 5);
            bag.Create(present);
            bag.Create(present1);
            present.Magic.ToString();
            present1.Magic.ToString();

            Assert.AreEqual(3.0d, present.Magic);
            Assert.AreEqual(present, bag.GetPresentWithLeastMagic());
        }
        [Test]
        public void GetPresent()
        {
            Bag bag = new Bag();
            Present present = new Present("doll", 3);
            bag.Create(present);

            var result = bag.GetPresent("doll");

            Assert.AreEqual(present, result);
            Assert.IsTrue(result != null);
           // Assert.AreEqual(bag.ToString(), "Presents.Bag");
        }
        [Test]
        public void RemoveShouldReturnFalseIfPresentNotInBag()
        {
            Bag bag = new Bag();
            Present present = new Present("Socks", 5.8);

            bool result = bag.Remove(present);

            Assert.AreEqual(false, result);
        }

        [Test]
        public void RemoveShouldReturnTrueIfPresentInBag()
        {
            Bag bag = new Bag();
            Present present = new Present("Socks", 5.8);

            bag.Create(present);

            bool result = bag.Remove(present);

            Assert.AreEqual(true, result);
        }
    }
}
