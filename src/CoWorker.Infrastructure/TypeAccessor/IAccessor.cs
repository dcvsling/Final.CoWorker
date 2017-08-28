
namespace CoWorker.Infrastructure.TypeAccessor
{

    public interface IAccessor
    {
        object Get();
        void Set(object val);
    }
}