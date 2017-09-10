
namespace CoWorker.DependencyInjection.Factory
{
    using CoWorker.Infrastructure.Cache;
    public interface IOptionsCache<T> : ICache<T> where T : class
    {
    }
}