using System;
namespace CoWorker.EntityFramework.EntityParts.ValueGenerators
{
    using Microsoft.EntityFrameworkCore.ChangeTracking;
    using Microsoft.EntityFrameworkCore.ValueGeneration;

    public class DatetimeValueGenerator : ValueGenerator<DateTime>
    {
        public override Boolean GeneratesTemporaryValues => true;

        public override DateTime Next(EntityEntry entry)
            => DateTime.Now;
    }
}
