using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Linq;

namespace Stealer
{
    public class Spy
    {
        public string StealFieldInfo(string investigateClass, params string[] requestedFields)
        {
            Type classType = Type.GetType(investigateClass);
            FieldInfo[] classField = classType.GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic |
                BindingFlags.Public);
            StringBuilder sb = new StringBuilder(); 
            object classInstance = Activator.CreateInstance(classType, new object[] {});
            sb.AppendLine($"Class under investigation: {investigateClass}");

            foreach (var item in classField.Where(f => requestedFields.Contains(f.Name)))
            {
                sb.AppendLine($"{item.Name} = {item.GetValue(classInstance)}");
            }
            return sb.ToString();
        }
    }
}
