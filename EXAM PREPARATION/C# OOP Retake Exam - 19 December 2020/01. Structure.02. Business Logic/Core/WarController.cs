using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WarCroft.Constants;
using WarCroft.Entities.Characters;
using WarCroft.Entities.Characters.Contracts;
using WarCroft.Entities.Items;

namespace WarCroft.Core
{
	public class WarController
	{
		private readonly List<Character> characters;
		private readonly List<Item> items;
		public WarController()
		{
			characters = new List<Character>();
			items = new List<Item>();
		}

		public string JoinParty(string[] args)
		{
			string characterType = args[0];
			string name= args[1];
			Character  character;
            if (characterType == "Warrior")
            {
				character = new Warrior(name);
			}
            else if (characterType == "Priest")
            {
				character = new Priest(name);
			}
            else
            {
				throw new ArgumentException(string.Format(ExceptionMessages.InvalidCharacterType, characterType));
            }
			characters.Add(character);
			return string.Format(SuccessMessages.JoinParty, name);
		}

		public string AddItemToPool(string[] args)
		{
			string itemName = args[0];
			Item item;
            if (itemName == "FirePotion")
            {
				item = new FirePotion();
			}
            else if (itemName == "HealthPotion")
            {
				item = new HealthPotion();
			}
            else
            {
				throw new ArgumentException(string.Format(ExceptionMessages.InvalidItem, itemName));
			}
			items.Add(item);
			return string.Format(SuccessMessages.AddItemToPool, itemName);
		}

		public string PickUpItem(string[] args)
		{
			string characterName = args[0];
			Character character = characters.FirstOrDefault(x=>x.Name == characterName);

			if (character == null)
            {
				throw new ArgumentException(string.Format(ExceptionMessages.CharacterNotInParty, characterName));
			}
            if (!items.Any())
            {
				throw new InvalidOperationException(ExceptionMessages.ItemPoolEmpty);
            }
			Item item = items.LastOrDefault();
			items.Remove(item);
			character.Bag.AddItem(item);

			return string.Format(SuccessMessages.PickUpItem, characterName, item.GetType().Name);
		}

		public string UseItem(string[] args)
		{
			string characterName = args[0];
			string itemName = args[1];

			Character character = characters.FirstOrDefault(x => x.Name == characterName);
            if (character == null)
            {
				throw new ArgumentException(string.Format(ExceptionMessages.CharacterNotInParty, characterName));
			}
			Item item = character.Bag.GetItem(itemName);  
			character.UseItem(item);
			return string.Format(SuccessMessages.UsedItem, characterName,itemName);
		}

		public string GetStats()
		{
			StringBuilder sb = new StringBuilder();
			var character = characters.OrderByDescending(x => x.IsAlive).ThenByDescending(x => x.Health);
            foreach (var item in character)
            {
				sb.AppendLine(string.Format(SuccessMessages.CharacterStats,
					item.Name, item.Health, item.BaseHealth, item.Armor, item.BaseArmor, item.IsAlive ? "Alive" : "Dead"));
            }
			return sb.ToString().TrimEnd();
		}

		public string Attack(string[] args)
		{
			string attackerName = args[0];
			string receiverName = args[1];

			Character attacker = characters.FirstOrDefault(x => x.Name == attackerName);
			if (attacker == null)
			{
				throw new ArgumentException(string.Format(ExceptionMessages.CharacterNotInParty, attackerName));

			}
			Character receiver = characters.FirstOrDefault(x => x.Name == receiverName);
			if (receiver == null)
			{
				throw new ArgumentException(string.Format(ExceptionMessages.CharacterNotInParty, receiverName));

			}

			if (!(attacker is IAttacker))
			{
				throw new ArgumentException(ExceptionMessages.AttackFail, attackerName);
			}
			((IAttacker)attacker).Attack(receiver);
			string result = string.Format(SuccessMessages.AttackCharacter, attackerName, receiverName, attacker.AbilityPoints,
				receiverName, receiver.Health, receiver.BaseHealth, receiver.Armor, receiver.BaseArmor );

            if (!receiver.IsAlive)
            {
				result += Environment.NewLine + string.Format(SuccessMessages.AttackKillsCharacter, receiverName);
            }
			return result;
		}

		public string Heal(string[] args)
		{
			string healerName = args[0];
			string healingReceiverName = args[1];

			Character healer = characters.FirstOrDefault(x => x.Name == healerName);
			if (healer == null)
			{
				throw new ArgumentException(string.Format(ExceptionMessages.CharacterNotInParty, healerName));
			}
			Character receiver = characters.FirstOrDefault(x => x.Name == healingReceiverName);
			if (receiver == null)
			{
				throw new ArgumentException(string.Format(ExceptionMessages.CharacterNotInParty, healingReceiverName));
			}

            if (!(healer is IHealer))
            {
				throw new ArgumentException(ExceptionMessages.HealerCannotHeal, healerName);
            }

			((IHealer)healer).Heal(receiver);

			string result = string.Format(SuccessMessages.HealCharacter, healerName, healingReceiverName, healer.AbilityPoints,
				healingReceiverName, receiver.Health);


			return result;
		}
	}
}
