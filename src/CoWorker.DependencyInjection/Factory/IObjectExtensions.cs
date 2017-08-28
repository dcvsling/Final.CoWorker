using System;
namespace CoWorker.DependencyInjection.Factory
{
    public interface IObjectExtensions<T> where T : class
    {
        void Invoke(string name, T options, Action<T> callback);
    }
}