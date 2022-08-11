using System;
using System.Reflection;
using NUnit.Framework;

public class HeroRepositoryTests
{
	[Test]
	public void HeroConstructor()
	{
		Hero hero = new Hero("Richie", 5);
		Assert.NotNull(hero.Name);
		Assert.AreEqual("Richie", hero.Name);
		Assert.AreEqual(5, hero.Level);
	}
	[Test]
	public void CreateMethodShouldThrowExceptionIfHeroIsNull()
	{
		Hero hero = null;
		HeroRepository repository = new HeroRepository();
		Assert.Throws<ArgumentNullException>(() => repository.Create(hero));
	}
	[Test]
	public void CreateHeroThrow()
	{
		Hero hero = new Hero("Richie", 5);
		HeroRepository repository = new HeroRepository();
		repository.Create(hero);
		Assert.Throws<InvalidOperationException>(() => repository.Create(hero));
	}
	[Test]
	public void CreateHero()
    {
		Hero hero = new Hero("Richie", 5);
		HeroRepository repository = new HeroRepository();
		repository.Create(hero);
		Assert.AreEqual(repository.Heroes.Count, 1);
	}
	[Test]
	public void RemoveMethodShouldThrowExceptionIfHeroIsNull()
	{
		Hero hero = new Hero("Richie", 5);
		HeroRepository repository = new HeroRepository();
		repository.Create(hero);
		Assert.Throws<ArgumentNullException>(() => repository.Remove(""));
		Assert.Throws<ArgumentNullException>(() => repository.Remove(" "));
		Assert.Throws<ArgumentNullException>(() => repository.Remove(null));
	}
	[Test]
	public void RemoveHeroCorrectly()
	{
		Hero hero = new Hero("Richie", 5);
		HeroRepository repository = new HeroRepository();
		repository.Create(hero);
		bool isRemoved = repository.Remove("Richie");
		Assert.AreEqual(repository.Heroes.Count, 0);
		Assert.IsTrue(isRemoved);
	}
	[Test]
	public void GetHeroWithHighestLevel()
	{
		HeroRepository repository = new HeroRepository();
		Hero hero = new Hero("Richie", 5);
		Hero hero1 = new Hero("Richko", 37);
	
		repository.Create(hero);
		repository.Create(hero1);

		var result = repository.GetHeroWithHighestLevel();

		Assert.AreEqual(hero1, result);
	}
	[Test]
	public void GetHero()
	{
		Hero hero = new Hero("Richie", 5);
		HeroRepository repository = new HeroRepository();
		repository.Create(hero);
		var result = repository.GetHero("Richie");

		Assert.AreEqual(hero, result);
		Assert.IsTrue(result != null);
		Assert.AreEqual(repository.ToString(), "HeroRepository");
	}
	[Test]
	public void HeroesPropertyShouldBeReadOnly()
	{
		var type = typeof(HeroRepository);
		PropertyInfo propertyInfo = type.GetProperty("Heroes");

		Assert.That(propertyInfo.CanWrite == false);
	}
}