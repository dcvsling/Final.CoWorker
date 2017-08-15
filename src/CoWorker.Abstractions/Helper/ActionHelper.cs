
using System.Threading.Tasks;
using CoWorker.Abstractions.Helper;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace System.Linq
{
    public static class ActionHelper
    {
        public static Action<T> Append<T>(this Action<T> action, Action<T> next)
            => t =>
            {
                action(t);
                next(t);
            };

        public static RequestDelegate Next(this RequestDelegate req, RequestDelegate add)
            => async ctx =>
            {
                await req(ctx).ConfigureAwait(true);
                await add(ctx).ConfigureAwait(false);
            };

        public static Func<T, T> Merge<T>(this Func<T, T> last, Func<T, T> next)
            => t => last(next(t));
		
        public static Func<Func<T, Task>, Func<T, Task>> Merge<T>(
            this Func<Func<T, Task>, Func<T, Task>> basemiddleware,
            Func<Func<T, Task>, Func<T, Task>> other
        )
            => Merge<Func<T,Task>>(basemiddleware,other);
		private readonly static Action EmptyAction = () => { };
        public static Action Empty() => EmptyAction;
        public static Action<T> Empty<T>() => x => EmptyAction();
        public static Action<T,T2> Empty<T,T2>() => (x,y) => EmptyAction();
        private readonly static Func<object> DefaultMethod = () => default;
        public static Func<object> Default() => DefaultMethod;
        public static Func<TResult> Default<TResult>() => () => default;
        public static Func<T,TResult> Default<T,TResult>() => x => Default<TResult>()();
        public static Func<T, T2,TResult> Default<T, T2,TResult>() => (x, y) => Default<TResult>()();
        public static Action<T> EmptyWithException<T>(Func<Exception> exgetter) =>
            x => { throw exgetter(); };

        public static Func<T> ToFunc<T>(this IObject<T> t) => () => t.Value;

        public static T GetValue<T>(this Action<Action<T>> action)
        {
            var options = default(T);
            action(x => options = x);
            return options;
        }
        
		private static Func<bool> TrueAction = () => true;
		public static Func<bool> AlwaysTrue() => TrueAction;
		public static Func<T, bool> AlwaysTrue<T>() => t => TrueAction();
		private static Func<bool> FalseAction = () => false;
		public static Func<bool> AlwaysFalse() => FalseAction;
		public static Func<T, bool> AlwaysFalse<T>() => t => FalseAction();
		public static TResult Using<TDisposable, TResult>(
            this TDisposable disposer, 
            Func<TDisposable, TResult> func,
            Action<Exception> error = null,
            Action final = null)
            where TDisposable : IDisposable
        {
            using (disposer)
            {
                try
                {
                    return func(disposer);
                }
                catch (Exception ex)
                {
                    error?.Invoke(ex);
                    throw;
                }
                finally
                {
                    final?.Invoke();
                }
            }
        }

        public static void Using<TDisposable>(
            this TDisposable disposer, 
            Action<TDisposable> action,
            Action<Exception> error = null,
            Action final = null)
            where TDisposable : IDisposable
        {
            using (disposer)
            {
                try
                {
                    action(disposer);
                }
                catch(Exception ex)
                {
                    error?.Invoke(ex);
                    throw;
                }
                finally
                {
                    final?.Invoke();
                }
            }
        }
    }
}
