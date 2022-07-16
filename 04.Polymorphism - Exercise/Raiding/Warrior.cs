using System;
using System.Collections.Generic;
using System.Text;

namespace Raiding
{
    public class Warrior : BaseHero
    {
        private const int WarriorPower = 100;
        public Warrior(string name, string type)
           : base(name, type)
        {
        }

        public override int Power => WarriorPower;

        public override string CastAbility()
          => $"{this.GetType().Name} - {this.Name} hit for {this.Power} damage";
    }
}
