using System;
using System.Collections.Generic;
using System.Text;

namespace CustomStack
{
    public class StackOfStrings : Stack<string>
    {

        public bool IsEmpty()
        {
            return Count == 0;
        }

        public Stack<string> AddRange(List<string> list)
        {
            foreach (var item in list)
            {
                Push(item);
            }
            return this;
        }
    }
}

