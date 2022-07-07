using System;

namespace CustomStack
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            StackOfStrings stackOfStrings = new StackOfStrings();
            Console.WriteLine(stackOfStrings.IsEmpty());

            stackOfStrings.AddRange(new System.Collections.Generic.List<string>()
            { "3", "2", "1"});
            Console.WriteLine(stackOfStrings.Pop());
            Console.WriteLine(stackOfStrings.Pop());
            Console.WriteLine(stackOfStrings.Pop());
        }
    }
}
