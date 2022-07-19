using System;
using System.Collections.Generic;
using System.Linq;

namespace _05._Play_Catch
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] input = Console.ReadLine().Split().Select(int.Parse).ToArray();
            int count = 0;
            while (count != 3)
            {
                string[] commands = Console.ReadLine().Split();
                string action = commands[0];
                try
                {
                    if (action == "Replace")
                    {
                        int index = int.Parse(commands[1]);
                        int element = int.Parse(commands[2]);
                        input[index] = element;
                    }
                    else if (action == "Print")
                    {
                        int startIndex = int.Parse(commands[1]);
                        int endIndex = int.Parse(commands[2]);
                        var rangeToPrint = new List<int>();
                        for (int i = startIndex; i <= endIndex; i++)
                        {
                            rangeToPrint.Add(input[i]);
                        }
                        Console.WriteLine(string.Join(", ", rangeToPrint));
                    }
                    else if (action == "Show")
                    {
                        int index = int.Parse(commands[1]);
                        Console.WriteLine(input[index]);
                    }
                }
                catch (IndexOutOfRangeException)
                {
                    Console.WriteLine("The index does not exist!");
                    count++;
                }
                catch (FormatException)
                {
                    Console.WriteLine("The variable is not in the correct format!");
                    count++;
                }
            }
            Console.WriteLine(string.Join(", ", input));
        }
    }
}
