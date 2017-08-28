
namespace CoWorker.Infrastructure.TypeAccessor
{
    using CoWorker.Infrastructure.Cache;
    using System;

    public class TypeMapper<T> : ITypeMapper<T>
    {
        private readonly PropertyAccessorStore _store;
        private readonly ICache<IAccessor> _cache;
        public TypeMapper(T t,PropertyAccessorStore store)
        {
            _store = store;
            _cache = new DictionaryCache<IAccessor>();
        }

        private IAccessor GetAccessor(string name)
            => _cache.GetOrAdd(
                name,
                () => new Accessor(_store.Get<T>(name), this));

        public TValue Get<TValue>(String name)
            => GetAccessor(name).Get<TValue>();
        public void Set(String name, Object val)
            => GetAccessor(name).Set(val);
    }
}