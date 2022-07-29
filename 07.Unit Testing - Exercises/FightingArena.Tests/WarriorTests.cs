namespace FightingArena.Tests
{
    using NUnit.Framework;

    [TestFixture]
    public class WarriorTests
    {
        [Test]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase(null)]
        public void WarriorNameShouldNotBeNullOrWhiteSpace(string name)
        {
            Assert.That(() => new Warrior(name, 50, 100),
                Throws.ArgumentException.With.Message
                    .EqualTo("Name should not be empty or whitespace!"));
        }

        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        public void WarriorDamageShouldNotBe0OrLess(int damage)
        {
            Assert.That(() => new Warrior("Maria", damage, 100),
                Throws.ArgumentException.With.Message
                    .EqualTo("Damage value should be positive!"));
        }

        [Test]
        public void WarriorHealthShouldNotBeNegative()
        {
            Assert.That(() => new Warrior("Maria", 50, -20),
                Throws.ArgumentException.With.Message
                    .EqualTo("HP should not be negative!"));
        }

        [Test]
        public void ConstructorShouldCreateValidWarrior()
        {
            Warrior warrior = new Warrior("Maria", 50, 100);

            Assert.That(warrior.Name == "Maria");
            Assert.That(warrior.Damage == 50);
            Assert.That(warrior.HP == 100);
        }

        [Test]
        [TestCase(10)]
        [TestCase(25)]
        public void AttackMethodShouldThrowExceptionIfWarriorHpLessOrEqualToMinHp(int HP)
        {
            Warrior warrior = new Warrior("Maria", 50, HP);
            Warrior enemy = new Warrior("Guest", 50, 50);

            Assert.That(() => warrior.Attack(enemy),
                Throws.InvalidOperationException.With.Message
                    .EqualTo("Your HP is too low in order to attack other warriors!"));
        }

        [Test]
        [TestCase(25)]
        [TestCase(15)]
        public void AttackMethodShouldThrowExceptionIfEnemyHpLessOrEqualTo30(int HP)
        {
            Warrior warrior = new Warrior("Maria", 50, 50);
            Warrior enemy = new Warrior("Guest", 50, HP);

            Assert.That(() => warrior.Attack(enemy),
                Throws.InvalidOperationException.With.Message
                    .EqualTo($"Enemy HP must be greater than 30 in order to attack him!"));
        }

        [Test]
        public void AttackMethodShouldThrowExceptionIfWarriorHpLessThanEnemyDamage()
        {
            Warrior warrior = new Warrior("Maria", 50, 50);
            Warrior enemy = new Warrior("Guest", 55, 50);

            Assert.That(() => warrior.Attack(enemy),
                Throws.InvalidOperationException.With.Message
                    .EqualTo($"You are trying to attack too strong enemy"));
        }

        [Test]
        public void AttackMethodShouldReduceWarriorHp()
        {
            Warrior warrior = new Warrior("Maria", 70, 100);
            Warrior enemy = new Warrior("Guest", 50, 50);

            warrior.Attack(enemy);

            Assert.That(warrior.HP, Is.EqualTo(50));
        }

        [Test]
        public void AttackMethodShouldReduceEnemyHp()
        {
            Warrior warrior = new Warrior("Maria", 50, 100);
            Warrior enemy = new Warrior("Guest", 50, 70);

            warrior.Attack(enemy);

            Assert.That(enemy.HP, Is.EqualTo(20));
        }
    }
}