using System;

namespace BizActionExample.Infa
{
    [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Module, AllowMultiple = true)]
    public class ConfigurationKeyAttribute : Attribute
    {
        public ConfigurationKeyAttribute(string name, string key, bool required)
        {
            Name = name;
            Key = key;
            Required = required;
        }

        public ConfigurationKeyAttribute(string name, string key) : this(name, key, true)
        {
        }

        public string Name { get; }

        public string Key { get; }

        public bool Required { get; }
    }
}