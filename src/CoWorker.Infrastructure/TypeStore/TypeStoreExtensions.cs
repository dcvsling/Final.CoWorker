﻿using System.Reflection;

namespace CoWorker.Infrastructure.TypeStore
{

    public static class TypeStoreExtensions
    {
        public static Assembly Load(this AssemblyName name) => Assembly.Load(name);
    }
}