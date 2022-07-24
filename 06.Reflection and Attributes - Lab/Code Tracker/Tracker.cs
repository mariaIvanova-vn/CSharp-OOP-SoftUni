using System;
using System.Collections.Generic;
using System.Text;

namespace AuthorProblem
{
    public class Tracker
    {
        public void PrintMethodsByAuthor()
        {
            Type type = typeof(StartUp);

            foreach (var method in type.GetMethods())
            {
                object[] attributes = method.GetCustomAttributes(false);
                foreach (var item in attributes)
                {
                    AuthorAttribute authorAttribute = item as AuthorAttribute;
                    if (authorAttribute != null)
                    {
                        Console.WriteLine($"{method.Name} is written by {authorAttribute.Name}");
                    }
                }
            }
        }
    }
}
