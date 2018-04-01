namespace Raindrop.Configuration
{
    using System;

    [AttributeUsage(AttributeTargets.Class)]
    public class ConfigurationNameAttribute : Attribute
    {
        public string Name { get; }

        public ConfigurationNameAttribute(string name)
        {
            Name = name;
        }
    }
}
