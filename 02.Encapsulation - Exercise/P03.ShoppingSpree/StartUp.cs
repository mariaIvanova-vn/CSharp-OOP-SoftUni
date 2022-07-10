using System;
using System.Collections.Generic;
using System.Linq;

namespace P03.ShoppingSpree
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            Dictionary<string, Person> personsDict = new Dictionary<string, Person>();
            Dictionary<string, Product> productDict = new Dictionary<string, Product>();
            string[] people = Console.ReadLine().Split(new char[] { ';', '=' }, StringSplitOptions.RemoveEmptyEntries);
            string[] products = Console.ReadLine().Split(new char[] { ';', '=' }, StringSplitOptions.RemoveEmptyEntries);
            try
            {
                for (int p = 0; p < people.Length; p+=2)
                {
                    string name = people[p];
                    decimal money= decimal.Parse(people[p+1]);
                    Person person = new Person(name, money);
                    personsDict.Add(name, person);
                }
                for (int i = 0; i < products.Length; i+=2)
                {
                    string name = products[i];
                    decimal cost = decimal.Parse(products[i + 1]);
                    Product product = new Product(name, cost);  
                    productDict.Add(name, product);
                }
                string command;
                while ((command=Console.ReadLine())!= "END")
                {
                    string[] commandInfo = command.Split();
                    string personName=commandInfo[0];
                    string productName = commandInfo[1];
                    Person person = personsDict[personName];
                    Product product = productDict[productName];

                    bool IsAdded=person.AddProduct(product);
                    if (!IsAdded)
                    {
                        Console.WriteLine($"{personName} can't afford {productName}");
                    }
                    else
                    {
                        Console.WriteLine($"{personName} bought {productName}");
                    }
                }
                foreach (var (name,person) in personsDict)
                {
                    if (person.Products.Count > 0)
                    {
                        string productInfo = string.Join(", ", person.Products.Select(x=>x.Name));
                        Console.WriteLine($"{name} - {productInfo}");
                    }
                    else
                    {
                        Console.WriteLine($"{name} - Nothing bought");
                    }
                    //string productInfo = person.Products.Count > 0
                    //     ? string.Join(", ", person.Products)
                    //     : "Nothing bought";
                    //Console.WriteLine($"{name} - {productInfo}");
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
        }
    }
}
