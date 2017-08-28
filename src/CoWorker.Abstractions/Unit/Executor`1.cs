
namespace CoWorker.Abstractions.Values
{
    using System;
    using System.Linq.Expressions;

    public struct Executor<T>
    {
        private Action<T> _executor;
        public Executor(Action<T> executor)
        {
            _executor = executor ?? Helper.Empty<T>();
        }

        public Action<T> Invoke => _executor;
        public static implicit operator Action<T>(Executor<T> exec) => exec.Invoke;
        public static implicit operator Executor<T>(Action<T> action) => new Executor<T>(action);
        public static implicit operator Expression<Action<T>>(Executor<T> exec)
            => Expression.Lambda<Action<T>>(Expression.Invoke(Expression.Constant(exec)));
        public static implicit operator Executor<T>(Expression<Action<T>> action)
            => new Executor<T>(action.Compile());
        public static Executor<T> operator >(Executor<T> left, Executor<T> right)
            => new Executor<T>(
                t => {
                    left.Invoke(t);
                    right.Invoke(t);
                });
        public static Executor<T> operator <(Executor<T> left, Executor<T> right)
            => new Executor<T>(t => {
                right.Invoke(t);
                left.Invoke(t);
            });
    }

}
