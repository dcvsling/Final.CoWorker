
namespace CoWorker.Infrastructure.TypeAccessor
{
    using CoWorker.Infrastructure.Cache;
    using System;
    public class PropertyAccessorStore
    {
        private readonly ICache<IPropertyAccessor> _cache;
        public PropertyAccessorStore()
        {
            _cache = new DictionaryCache<IPropertyAccessor>();
        }
        public IPropertyAccessor Get<T>(string name)
            => _cache.GetOrAdd(
                $"{typeof(T).FullName}.{name}" ,
                () => new PropertyAccessor(typeof(T).GetProperty(name)));

        public IPropertyAccessor GetOrAdd<T>(string name,Func<IPropertyAccessor> accessorGetter)
            => _cache.GetOrAdd(
                $"{typeof(T).FullName}.{name}", 
                accessorGetter);
    }
}