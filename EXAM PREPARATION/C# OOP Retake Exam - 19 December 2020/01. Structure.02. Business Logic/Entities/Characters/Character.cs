using System;
using System.Linq;

using WarCroft.Constants;
using WarCroft.Entities.Inventory;
using WarCroft.Entities.Items;

namespace WarCroft.Entities.Characters.Contracts
{
    public abstract class Character
    {
		private string name;
		private double health;
		private double armor;
		
		public Character(string name, double health, double armor, double abilityPoints, Bag bag)
        {
			this.Name = name;
			this.BaseHealth = health;
			this.Health = health;
			this.BaseArmor = armor;
			this.Armor = armor;
			this.AbilityPoints = abilityPoints;
			this.Bag = bag;           
		}

		public string Name
		{
			get { return name; }
			private set
			{
                if (string.IsNullOrWhiteSpace(value))
                {
					throw new ArgumentException("Name cannot be null or whitespace!");
				}
				name = value;
			}
		}
        public double BaseHealth { get; private set; }
		public double Health
		{
			get { return health; }
			 set
			{
				health = value;
				if (health<0)
				{
					health=0;
				}
                if (health>BaseHealth)
                {
					health = BaseHealth;	
                }
			}
		}
		public bool IsAlive { get; set; } = true;
        public double BaseArmor { get; private set; }
        public double Armor
		{
			get { return armor; }
			private set
			{
				armor = value;
				if (armor < 0)
				{
					armor = 0;
				}
			}
		}
		public double AbilityPoints  { get; private set; }
		public IBag Bag { get; private set; }	
        protected void EnsureAlive()
		{
			if (!this.IsAlive)
			{
				throw new InvalidOperationException(ExceptionMessages.AffectedCharacterDead);
			}
		}
		public void TakeDamage(double hitPoints)
        {
            if (!this.IsAlive)
            {
				return;
            }
			double leftoverHitPoints = 0;

			if (this.Armor - hitPoints < 0)
			{
				leftoverHitPoints = this.Armor - hitPoints;
			}
			this.Armor -= hitPoints;
			this.Health -= Math.Abs(leftoverHitPoints);

			if (this.Health == 0)
			{
				this.IsAlive = false;
			}
		}
		public void UseItem(Item item)
        {
			
            if (this.IsAlive)
            {
				item.AffectCharacter(this);
            }
        }
	}
}