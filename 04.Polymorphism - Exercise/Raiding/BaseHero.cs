
namespace Raiding
{
    public abstract class BaseHero
    {
        //private string name;
        //private int power;
        public BaseHero(string name, string type)
        {
            this.Name = name;
            this.Type = type;
        }

        public string Name { get; set; }
        public string Type { get; set; }
        public abstract int Power { get; }

        public abstract string CastAbility();
    }
}
