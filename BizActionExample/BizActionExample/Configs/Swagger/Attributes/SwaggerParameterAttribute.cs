using System;

namespace BizActionExample.Configs.Swagger.Attributes
{
    [AttributeUsage(AttributeTargets.Parameter, Inherited = false, AllowMultiple = true)]
    public class SwaggerParameterAttribute : Attribute
    {
        public SwaggerParameterAttribute()
        {
        }

        public SwaggerParameterAttribute(string description, bool required = false)
        {
            Description = description;
        }

        public SwaggerParameterAttribute(Type resourceType, string descriptionResourceName, bool required = false)
        {
            Description = new System.Resources.ResourceManager(resourceType).GetString(descriptionResourceName);
            Required = required;
        }

        //public Type DataType { get; set; }
        //public string ParameterType { get; set; }
        public string Description { get; private set; }

        public bool Required { get; set; } = false;
    }
}