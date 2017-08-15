using System.Linq;

namespace System
{
    using System.Collections.Generic;
    public static class Helper
    {
        public static IEnumerable<T> Group<T>(params T[] ts) => ts;

        public static Func<object> CreateFactory(Type type)
            => () => Activator.CreateInstance(type);
        public static Func<T> CreateFactory<T>()
            => () => CreateFactory(typeof(T)).As<T>();
        public static Func<object> CreateFactory(Type type,params object[] args)
            => () => Activator.CreateInstance(type,args);
        public static Func<T> CreateFactory<T>(params object[] args)
            => () => CreateFactory(typeof(T), args).As<T>();

        public static Action<T> If<T>(
          Func<T, bool> predicate,
          Action<T> truecase,
          Action<T> falsecase = null)
          => t => (predicate?.Invoke(t) ?? false ? truecase : falsecase)?.Invoke(t);

        public static Func<T, TResult> If<T, TResult>(
            Func<T, bool> predicate,
            Func<T, TResult> truecase,
            Func<T, TResult> falsecase = null)
            => t => ((predicate?.Invoke(t) ?? false ? truecase : falsecase) ?? (x => default))(t);

        public static TDelegate Create<TDelegate>(TDelegate invoker) => invoker;
    }
}
