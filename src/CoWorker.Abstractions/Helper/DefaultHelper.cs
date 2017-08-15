namespace System.Linq
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

	public static class DefaultHelper
    {
		public static T NotNull<T>(this IObject<T> t, Func<T, Exception> throwIfNull)
			=> ReferenceEquals(t.Value, null) ? throw throwIfNull(t.Value) : t.Value;
        public static T DefaultIfNull<T>(this IObject<T> target, Func<T> getter) where T : class
            => target.Value ?? getter();

        public static T DefaultIfNull<T>(this IObject<T> target, T val) where T : class
            => target.Value ?? val;

        public static Action<T> DefaultIfNull<T>(this Action<T> target, Action<T> val = null)
            => target ?? val ?? ActionHelper.Empty<T>();

        public static IEnumerable<T> EmptyIfNull<T>(this object t)
            where T : class
            => ReferenceEquals(t, null)
                ? Enumerable.Empty<T>()
                : t as IEnumerable<T> ?? Enumerable.Empty<T>();
        
        public static object Default(this Type type)
            => type.GetTypeInfo().IsValueType || type == typeof(string)
                ? Activator.CreateInstance(type) : null;

        public static IEnumerable<T> Default<T>(this Type type)
            => new[] { (T)type.Default() };

        public static IEnumerable<T> AsSingleEnumerable<T>(this IObject<T> t) => new T[] { t.Value };
    }
}
