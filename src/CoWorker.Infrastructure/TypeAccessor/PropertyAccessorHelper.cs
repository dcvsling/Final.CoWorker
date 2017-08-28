using System.Reflection;
using System.Linq;
using CoWorker.Infrastructure.Cache;
using Microsoft.Extensions.Options;
using System;

namespace CoWorker.Infrastructure.TypeAccessor
{
    public static class PropertyAccessorHelper
    {
        public static T Get<T>(this IAccessor accessor)
            => accessor.Get().As<T>();
        public static T Get<T>(this IPropertyAccessor accessor,object obj)
            => accessor.Get(obj).As<T>();
    }
}