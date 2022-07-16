using System;
using System.Collections.Generic;
using System.Text;

namespace Raiding
{
    public class Rogue : BaseHero
    {
        private const int RoguePower = 80;
        public Rogue(string name, string type)
            : base(name, type)
        {
        }

        public override int Power => RoguePower;

        public override string CastAbility()
           => $"{this.GetType().Name} - {this.Name} hit for {this.Power} damage";
    }
}
