namespace Raindrop.Configuration
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;

    using Newtonsoft.Json.Linq;

    using Raindrop.Utility;

    public class ConfigurationProvider : IConfigurationProvider
    {
        private readonly IReadOnlyDictionary<string, JProperty> _configurationItems;
        private readonly IDictionary<Type, object> _parsedConfigurations = new Dictionary<Type, object>();

        public ConfigurationProvider(IFileStreamProvider fileStreamProvider)
        {
            _configurationItems =
                Assembly.GetExecutingAssembly().Location
                    .Map(Path.GetDirectoryName)
                    .Map(x => Path.Combine(x, "raindrop.config.json"))
                    .Map(fileStreamProvider.OpenFileForRead)
                    .Map(x => new StreamReader(x))
                    .Use(x => x.ReadToEnd())
                    .Map(JObject.Parse)
                    .Properties()
                    .ToDictionary(k => k.Name, v => v);
        }

        public TConfiguration GetConfiguration<TConfiguration>()
        {
            var type = typeof(TConfiguration);

            return
                (TConfiguration)
                (
                    _parsedConfigurations.ContainsKey(type)
                    ? (TConfiguration)_parsedConfigurations[type]
                    : _parsedConfigurations[type] = ParseConfiguration<TConfiguration>());
        }

        private TConfiguration ParseConfiguration<TConfiguration>() =>
            typeof(TConfiguration)
            .Map(GetConfigurationName)
            .Map(x => _configurationItems[x])
            .First
            .ToObject<TConfiguration>();

        private string GetConfigurationName(Type configurationType) =>
            configurationType.GetCustomAttribute<ConfigurationNameAttribute>()
            ?.Name
            ?? configurationType.FullName;
    }
}
