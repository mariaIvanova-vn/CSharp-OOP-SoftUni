using System;
using System.Collections.Generic;
using System.Text;

namespace Raiding
{
    public class Druid : BaseHero
    {
        private const int DruidPower = 80;

        public Druid(string name, string type) 
            : base(name, type)
        {
        }

        public override int Power => DruidPower;

        public override string CastAbility() 
            => $"{this.GetType().Name} - {this.Name} healed for {this.Power}";
    }
}
