using System.Reflection;

namespace CoWorker.Models.HostingStartupBase
{

    public static class TypeStoreExtensions
    {
        public static Assembly Load(this AssemblyName name) => Assembly.Load(name);
    }
}
