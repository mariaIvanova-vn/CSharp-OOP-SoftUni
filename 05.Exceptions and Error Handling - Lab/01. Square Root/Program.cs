using System;

namespace _01._Square_Root
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int number=int.Parse(Console.ReadLine());
            try
            {
                if (number<0)
                {
                    throw new ArgumentException("Invalid number.");
                }
                double result = Math.Sqrt(number);
                Console.WriteLine(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.WriteLine("Goodbye.");
            }
        }
    }
}
