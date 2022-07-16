using System;
using System.Collections.Generic;
using System.Text;

namespace Raiding
{
    public class Paladin : BaseHero
    {
        private const int PaladinPower = 100;
        public Paladin(string name, string type)
            : base(name, type)
        {
        }

        public override int Power => PaladinPower;

        public override string CastAbility() 
            => $"{this.GetType().Name} - {this.Name} healed for {this.Power}";
    }
}
