using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CoWorker.EntityFramework.EntityParts.ValueGenerators
{
    public interface IValueGeneratorProvider
    {
        string PropertyName { get; }
        object Create(EntityEntry entry, IProperty property);
    }
}
