using System;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CoWorker.EntityFramework.EntityParts.ValueGenerators
{
    public class ModifyDateValueGenerator : IValueGeneratorProvider
    {
        public String PropertyName => EntityFrameworkDefault.ModifyDate;

        public Object Create(EntityEntry entry, IProperty property)
            => DateTime.Now;
    }
}
