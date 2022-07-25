using System;
using System.Collections.Generic;
using System.Text;

namespace ValidationAttributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public abstract class MyValidationAttribute : Attribute
    {
        public abstract bool IsValid(object value);
    }
    public class MyRangeAttribute : MyValidationAttribute
    {
        private readonly int min;
        private readonly int max;
        public MyRangeAttribute(int min, int max)
        {
            this.min = min;
            this.max = max;
        }

        public override bool IsValid(object value)
        {
            int currValue=(int)value;
            return currValue>=min && currValue<=max;
        }
    }
    public class MyRequiredAttribute : MyValidationAttribute
    {
        public override bool IsValid(object value) => value!=null;  
    }
}
