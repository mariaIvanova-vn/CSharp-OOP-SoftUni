using System;

namespace AuthorProblem
{
    [AuthorAttribute("Maria")]
    public class StartUp
    {
        [AuthorAttribute("Maria")]
        static void Main(string[] args)
        {
            Tracker tracker = new Tracker();
            tracker.PrintMethodsByAuthor();
        }
    }
}
