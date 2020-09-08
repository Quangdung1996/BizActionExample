using System.Text;

namespace BizActionExample.Domain
{
    public class ChangeValue
    {
        public ChangeValue(string propertyName, string description, string oldValue, string newValue)
        {
            PropertyName = propertyName;
            Description = description;
            OldValue = oldValue;
            NewValue = newValue;
        }

        public string PropertyName { get; }

        public string Description { get; }

        public string OldValue { get; }

        public string NewValue { get; }

        public override string ToString()
        {
            var result = new StringBuilder();
            result.AppendFormat("{0}({1}),", PropertyName, Description);
            result.AppendFormat("OldValue:{0},NewValue:{1}", OldValue, NewValue);
            return result.ToString();
        }
    }
}