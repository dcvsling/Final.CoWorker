using CoWorker.DependencyInjection.Decorator;

namespace CoWorker.DependencyInjection.Factory
{
    using System.Linq;
    using System;
    using System.Collections.Generic;
    using CoWorker.DependencyInjection.Abstractions;

    public class DecoratorFactoryExtensions<T> : IObjectExtensions<T>
        where T : class
    {
        private readonly IEnumerable<IDecoratorOptions<T>> _options;

        public DecoratorFactoryExtensions(IEnumerable<IDecoratorOptions<T>> options)
        {
            this._options = options;
        }

        public void Invoke(String name, T options, Action<T> callback)
        {
            if (!_options.Any())
            {
                callback(options);
                return;
            }
            _options
                .Select(GetDecorator)
                .Aggregate(Merge)
                .Invoke(options,callback);

            Action<T, Action<T>> GetDecorator(IDecoratorOptions<T> decorator)
                => (o, cb) => decorator.Decorate(name, o, cb);

            Action<T, Action<T>> Merge(Action<T, Action<T>> left, Action<T, Action<T>> right)
                => (o, cb) => left(o, x => right(x, cb));
        }
    }
}