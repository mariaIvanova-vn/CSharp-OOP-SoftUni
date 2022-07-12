namespace Telephony
{
    using Core;
    using IO;
    using IO.Interfaces;

    public class StartUp
    {
        static void Main(string[] args)
        {
            IReader reader = new ConsoleReader();
            IWriter writer = new ConsoleWriter();
            new Engine(reader, writer).Start();
        }
    }
}
