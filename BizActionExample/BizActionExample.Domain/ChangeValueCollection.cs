using System;
using System.Collections.Generic;
using System.Text;

namespace BizActionExample.Domain
{
    public class ChangeValueCollection : List<ChangeValue>
    {
       
        public void Add(string propertyName, string description, string oldValue, string newValue)
        {
            if (string.IsNullOrWhiteSpace(propertyName))
                return;
            Add(new ChangeValue(propertyName, description, oldValue, newValue));
        }

   
        public override string ToString()
        {
            var result = new StringBuilder();
            foreach (var item in this)
                result.AppendLine(item.ToString());
            return result.ToString();
        }
    }
}
