﻿using System;
using System.Collections.Generic;

namespace _02._Enter_Numbers
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<int> numbers = new List<int>();
            int start = 1;
            int end = 100;

            while (numbers.Count != 10)
            {
                try
                {
                    start = ReadNumber(start, end);
                    numbers.Add(start);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            Console.WriteLine(string.Join(", ", numbers));

        }
        public static int ReadNumber(int start, int end)
        {
            string number = Console.ReadLine();
            if (!int.TryParse(number, out int result))
            {
                throw new FormatException("Invalid Number!");
            }
            if (result <= start || result >= end)
            {
                throw new ArgumentException($"Your number is not in range {start} - 100!");
            }
            return result;
        }
    }
}
