using System.Runtime.ExceptionServices;
using System.Threading;
using Microsoft.AspNetCore.Hosting;
using System.Threading.Tasks;
using System.Composition.Convention;
using System.Reflection;
using System.Linq;
using Microsoft.Extensions.DependencyModel;
using System;
using System.Composition.Hosting;
using System.Collections.Generic;

namespace CoWorker.Models.HostingStartupBase
{
    public class MEFProvider : IServiceProvider
    {
        private object _locker;
        private IEnumerable<Assembly> _assemblies;
        private ContainerConfiguration _config;
        private CompositionHost _host;
        private Task task;
        private readonly WebHostBuilderContext _context;

        public MEFProvider(WebHostBuilderContext context)
        {
            _config = new ContainerConfiguration();
            _host = default;
            task = Task.Run(() => ImportAssembly());
            _locker = new object();
            this._context = context;
        }


        public CompositionHost CreateHost()
        {
            _config = new ContainerConfiguration();
            task.Wait();
            _config.WithAssemblies(_assemblies);
            CreateBuilder(b => b.ForTypesMatching<IHostingStartup>(
                t => typeof(IHostingStartup).IsAssignableFrom(t) && t != typeof(MEFHostingStartup))
                .Export<IHostingStartup>()
                .NotifyImportsSatisfied(t => true));
            _host = _config.CreateContainer();
            return _host;
        }

        public Object GetService(Type serviceType) => _host.GetExport(serviceType);

        public void CreateBuilder(Action<ConventionBuilder> config)
        {
            var builder = new ConventionBuilder();
            config(builder);
            _config.WithDefaultConventions(builder);
        }

        private void ImportAssembly()
        {
            _assemblies = DependencyContext.Default
                .GetDefaultAssemblyNames()
                .Where(x => x.Name.StartsWith(nameof(CoWorker), StringComparison.OrdinalIgnoreCase)
                    && x != this.GetType().Assembly.GetName())
                .Select(Assembly.Load);
        }
    }
}
