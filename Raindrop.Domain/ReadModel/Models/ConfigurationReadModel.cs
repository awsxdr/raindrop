namespace Raindrop.Domain.ReadModel.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Utility;

    public class ConfigurationReadModel : BaseReadModel
    {
        private static readonly IReadOnlyList<Action<ConfigurationReadModel, ConfigurationReadModel>> CloneMethods =
            typeof(ConfigurationReadModel)
                .GetProperties()
                .Select(x => new { Property = x, GetMethod = x.GetGetMethod(), SetMethod = x.GetSetMethod(true) })
                .Select(x =>
                    (Action<ConfigurationReadModel, ConfigurationReadModel>)((from, to) =>
                        x.SetMethod.Invoke(
                            to,
                            new[]
                            {
                                x.GetMethod.Invoke(from, new object[0])
                            })))
                .ToList();

        public string DefaultGameNameFormat { get; private set; }

        private ConfigurationReadModel()
            : base(Guid.Empty)
        { }

        public ConfigurationReadModel SetDefaultGameNameFormat(string value) =>
            Clone()
            .Tee(x => x.DefaultGameNameFormat = value);

        private ConfigurationReadModel Clone() =>
            new ConfigurationReadModel()
            .Tee(x => CloneMethods.ForEach(m => m(this, x)).Evaluate());
    }
}
