using System;
using System.Collections.Generic;

namespace Raiding
{
    public class Program
    {
        static void Main(string[] args)
        {
            List<BaseHero> raidGroup = new List<BaseHero>();
            BaseHero hero = null;

            int n = int.Parse(Console.ReadLine());
            for (int i = 0; i < n; i++)
            {
               string heroName = Console.ReadLine();
               string heroType = Console.ReadLine();

                    if (heroType == "Druid")
                    {
                        hero = new Druid(heroName, heroType);
                    }
                    else if (heroType == "Paladin")
                    {
                        hero = new Paladin(heroName, heroType);
                    }
                    else if (heroType == "Rogue")
                    {
                        hero = new Rogue(heroName, heroType);
                    }
                    else if (heroType == "Warrior")
                    {
                        hero = new Warrior(heroName, heroType);
                    }
                    else
                    {
                        Console.WriteLine("Invalid hero!");
                    i--;
                        continue;
                    }


                raidGroup.Add(hero);
            }
            int bossPower = int.Parse(Console.ReadLine());
            int sum = 0;

            foreach (var baseHero in raidGroup)
            {
                Console.WriteLine(baseHero.CastAbility());
                sum += baseHero.Power;
            }
            Console.WriteLine(sum >= bossPower
                ? "Victory!"
                : "Defeat...");
        }
    }
}
