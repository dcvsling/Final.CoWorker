using System.Linq;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CoWorker.EntityFramework.EntityParts.ValueGenerators
{
    public class ValueGenFactory : ValueGeneratorFactory
    {
        private IDictionary<string, IValueGeneratorProvider> map;
        public ValueGenFactory(IEnumerable<IValueGeneratorProvider> providers)
        {
            this.map = providers.ToDictionary(x => x.PropertyName, x => x);
        }
        public override ValueGenerator Create(IProperty property)
            => map.ContainsKey(property.Name)
                ? Create(property.ClrType, entry => map[property.Name].Create(entry, property))
                : throw new NotSupportedException($"{property.Name} has not support ValueGeneratorProvider");

        private ValueGenerator Create(Type type, Func<EntityEntry, object> getter, bool isTemporary = true)
            => Activator.CreateInstance(
                    typeof(AnonymousValueGenerator<>).MakeGenericType(type),
                    getter,
                    isTemporary) as ValueGenerator;

        internal class AnonymousValueGenerator<T> : ValueGenerator<T>
        {
            private readonly Func<EntityEntry,object> _getter;

            public AnonymousValueGenerator(Func<EntityEntry,object> getter,bool isTemporary = true)
            {
                GeneratesTemporaryValues = isTemporary;
                _getter = getter;
            }
            public override Boolean GeneratesTemporaryValues { get; }

            public override T Next(EntityEntry entry)
                => _getter(entry) is T result ? result : default(T);
        }
    }
}
