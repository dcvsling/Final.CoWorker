using System.Linq;
using Microsoft.Extensions.Logging;

namespace CoWorker.DependencyInjection.Configuration
{
    using System.Linq;
    using System.Reflection;
    using System;
    using Microsoft.Extensions.Options;
    using Microsoft.Extensions.Configuration;
    using System.Collections.Generic;

    public class ConfigurationConfigureOptions<TOptions> : IConfigureNamedOptions<TOptions>
        where TOptions : class
    {
        private readonly IConfiguration _config;
        private readonly Type _type = typeof(TOptions);
        public ConfigurationConfigureOptions(IConfiguration config)
        {
            _config = config;
        }
        public void Configure(String name, TOptions options)
            => Bind(GetConfigurationByName(name), options);
                
        public void Configure(TOptions options)
            => Configure(string.Empty,options);

        private IConfigurationSection GetConfigurationByName(string name = null)
            => !string.IsNullOrEmpty(name)
                ? _config.GetSection(name) ?? GetConfigurationSectionByName()
                : GetConfigurationSectionByName();

        private IConfigurationSection GetConfigurationSectionByName()
        {
            var name = _type.Name.EndsWith("Options") ? _type.RemovePostfixName() : _type.Name;
            return GetExistConfigurationByNamespace(name, _type.Namespace.Split('.').Reverse());
        }

        private IConfigurationSection GetExistConfigurationByNamespace(string name,IEnumerable<string> prefixs)
        {
            if (!prefixs.Any()) return default;
            var type = typeof(TOptions);
            return _config.GetSection(name)
                    ?? GetExistConfigurationByNamespace(
                        $"{prefixs.First()}:{name}",
                        prefixs.Skip(1));
        }

        private void Bind(IConfigurationSection section,TOptions options)
            => section?.Bind(options);
    }
}