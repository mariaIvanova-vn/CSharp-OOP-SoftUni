using Heroes.Models.Contracts;
using Heroes.Models.Heroes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Heroes.Models.Map
{
    public class Map : IMap
    {
        public string Fight(ICollection<IHero> players)
        {
            //var knights = new List<Knight>();
            //var barbarians = new List<Barbarian>();
            ////var knightsWin = false;
            ////var barbariansWin = false;
            //foreach (var player in players)
            //{
            //    if (player.IsAlive)
            //    {
            //        if (player is Knight knight)
            //        {
            //            knights.Add(knight);  
            //        }
            //        else if (player is Barbarian barbarian)
            //        {
            //            barbarians.Add(barbarian);
            //        }
            //    }
            //}
            //var continueBattle = true;

            //while (continueBattle)
            //{
            //    var allKnightsDead = true;
            //    var allBarbariansDead = true;

            //    var aliveKnights = 0;
            //    var aliveBarbarians = 0;

            //    foreach(var knight in knights)
            //    {
            //        if (knight.IsAlive)
            //        {
            //            allKnightsDead = false;
            //            aliveKnights++;

            //            foreach (var barbarian in barbarians)
            //            {
            //                barbarian.TakeDamage(knight.Weapon.DoDamage());
            //            }
            //        }
            //    }
            //    foreach(var barbarian in barbarians)
            //    {
            //        if (barbarian.IsAlive)
            //        {
            //            allBarbariansDead = false;
            //            aliveBarbarians++;

            //            foreach (var knight in knights)
            //            {
            //                knight.TakeDamage(barbarian.Weapon.DoDamage());
            //            }
            //        }
            //    }
            //    if (allKnightsDead)
            //    {
            //        int casualties = knights.Count(x => x.IsAlive == false);
            //        return $"The knights took {casualties} casualties but won the battle.";
            //    }
            //    if(allBarbariansDead)
            //    {
            //        int casualties = barbarians.Count(x => x.IsAlive == false);
            //        return $"The barbarians took {casualties} casualties but won the battle.";
            //    }
            //}   
            //return "something wrong";

            List<IHero> knights = players.Where(p => p.GetType().Name == "Knight").ToList();
            List<IHero> barbarians = players.Where(p => p.GetType().Name == "Barbarian").ToList();

            bool knightsAreAlive = knights.Any(k => k.IsAlive == true);
            bool knightsWin = false;
            bool barbariansWin = false;
            while (true)
            {
                foreach (var knight in knights.Where(k => k.IsAlive == true))
                {
                    foreach (var barbarian in barbarians.Where(b => b.IsAlive == true))
                    {
                        barbarian.TakeDamage(knight.Weapon.DoDamage());
                    }
                    if (barbarians.All(b => b.IsAlive == false))
                    {
                        knightsWin = true;
                        break;
                    }
                }

                if (knightsWin)
                {
                    break;
                }
                foreach (var barbarian in barbarians.Where(b => b.IsAlive == true))
                {
                    foreach (var knight in knights.Where(k => k.IsAlive == true))
                    {
                        knight.TakeDamage(barbarian.Weapon.DoDamage());
                    }
                    if (knights.All(k => k.IsAlive == false))
                    {
                        barbariansWin = true;
                        break;
                    }
                }

                if (barbariansWin)
                {
                    break;
                }
            }

            if (knightsWin)
            {
                int casualties = knights.Count(x => x.IsAlive == false);
                return $"The knights took {casualties} casualties but won the battle.";
            }
            else
            {
                int casualties = barbarians.Count(x => x.IsAlive == false);
                return $"The barbarians took {casualties} casualties but won the battle.";
            }
        }
    }
}
