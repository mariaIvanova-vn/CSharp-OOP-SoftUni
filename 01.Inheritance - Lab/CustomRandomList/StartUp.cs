using System;

namespace CustomRandomList
{
    public class StartUp
    {
        static void Main(string[] args)
        {
           RandomList strings = new RandomList();

            strings.Add("name");
            strings.Add("name1");
            strings.Add("name2");

            Console.WriteLine(strings.RandomString());
        }
    }
}
