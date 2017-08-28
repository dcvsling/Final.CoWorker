using System.Linq;

namespace CoWorker.DependencyInjection.Abstractions
{
    public interface IObjectFactory<T> where T : class
    {
        T Create(System.String name);
    }
}