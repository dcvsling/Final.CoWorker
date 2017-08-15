using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyModel;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

namespace CoWorker.Infrastructure.TypeStore
{

    public static class TypeStoreExtensions
    {
        public static Assembly Load(this AssemblyName name) => Assembly.Load(name);
    }
}
