using System;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CoWorker.EntityFramework.EntityParts.ValueGenerators
{

    public class CreateDateValueGenerator : IValueGeneratorProvider
	{
        public String PropertyName => EntityFrameworkDefault.CreateDate;

        public Object Create(EntityEntry entry, IProperty property)
        {
            DateTime? result = DateTime.Now;
            var current = entry.CurrentValues.GetValue<DateTime?>(PropertyName);
            if (null != current) result = current;
            var dbval = entry.OriginalValues.GetValue<DateTime?>(PropertyName);
            if (dbval is DateTime?) result = dbval;
            return result;
        }
	}
}
