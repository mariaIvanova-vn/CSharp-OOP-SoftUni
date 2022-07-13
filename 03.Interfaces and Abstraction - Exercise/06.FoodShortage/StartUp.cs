using System;
using System.Collections.Generic;
using System.Linq;

namespace FoodShortage
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            Dictionary<string, IBuyer> buyerList = new Dictionary<string, IBuyer>();
            IBuyer buyer = null;

            int n = int.Parse(Console.ReadLine());
            for (int i = 0; i < n; i++)
            {
                string[] buyersInfo = Console.ReadLine().Split();
                string name = buyersInfo[0];
                int age= int.Parse(buyersInfo[1]);
                
                if (buyersInfo.Length == 3)
                {
                    string group = buyersInfo[2];
                    buyer = new Rebel(name, age, group);
                }else if (buyersInfo.Length == 4)
                {
                    string id = buyersInfo[2];
                    string birthdate= buyersInfo[3];    
                    buyer = new Citizen(name, age, id, birthdate);
                }
                buyerList[name] = buyer;
            }
            string buyerName = Console.ReadLine();

            while (buyerName != "End")
            {
                if (buyerList.ContainsKey(buyerName))
                {
                    buyerList[buyerName].BuyFood();
                }

                buyerName = Console.ReadLine();
            }

            int totalFoodBought = buyerList.Values.Sum(x => x.Food);

            Console.WriteLine(totalFoodBought);
        }
    }
}
