
namespace CoWorker.DependencyInjection.Factory
{
    using System.Linq;
    using Microsoft.Extensions.DependencyInjection;
    using CoWorker.DependencyInjection.Abstractions;
    using Microsoft.Extensions.Options;
    using System;
    using System.Collections.Generic;
    using CoWorker.Infrastructure.Cache;

    public class ConfigureOptionsObjectConfigure<T> : IObjectConfigure<T>
        where T : class
    {
        private readonly IEnumerable<IConfigureOptions<T>> _configs;
        private readonly IEnumerable<IPostConfigureOptions<T>> _posts;

        public ConfigureOptionsObjectConfigure(
            IEnumerable<IConfigureOptions<T>> configs,
            IEnumerable<IPostConfigureOptions<T>> posts)
        {
            _configs = configs;
            _posts = posts;
        }

        public void Configure(String name, T options)
            => GetConfiguredOptions(name).Append(GetPostConfiguredOptions(name)).Invoke(options);

        private Action<T> GetConfiguredOptions(string name)
           => !_configs.Any() 
                ? Helper.Empty<T>()
                : _configs.Select(
                     x => x is IConfigureNamedOptions<T> named
                         ? (Action<T>)(o => named.Configure(name, o))
                         : (x.Configure))
                     .DefaultIfEmpty(Helper.Empty<T>())
                     .Aggregate(ActionHelper.Append);

        private Action<T> GetPostConfiguredOptions(string name)
            => _posts
                .Select<IPostConfigureOptions<T>, Action<T>>(x => o => x.PostConfigure(name, o))
                .DefaultIfEmpty(Helper.Empty<T>())
                .Aggregate(ActionHelper.Append);
    }
}