using NUnit.Framework;

namespace Skeleton.Tests
{
    [TestFixture]
    public class DummyTests
    {
        [Test]
        public void DummyShouldLooseHealthWhenAttacked()
        {
            Dummy dummy = new Dummy(100, 100);
            dummy.TakeAttack(50);

            Assert.AreEqual(50, dummy.Health);
        }
        [Test]
        public void DeadDummyShouldThrowExceptionWhenAttacked()
        {
            Dummy dummy = new Dummy(0, 100);
            Assert.That(() => dummy.TakeAttack(50), Throws.InvalidOperationException.With.Message.EqualTo("Dummy is dead."));
        }
        [Test]
        public void DeadDummyShouldGiveXp()
        {
            Dummy dummy = new Dummy(0, 100);
            Assert.AreEqual(100, dummy.GiveExperience());
        }
        [Test]
        public void AliveDummyShouldNotGiveXp()
        {
            Dummy dummy = new Dummy(100, 100);

            Assert.That(() => dummy.GiveExperience(), Throws.InvalidOperationException.With.Message.EqualTo("Target is not dead."));

        }
    }
}