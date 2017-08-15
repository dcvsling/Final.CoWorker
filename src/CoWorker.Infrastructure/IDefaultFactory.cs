using System;
using System.Linq;

namespace CoWorker.Infrastructure.DefaultFactory
{
    public interface IDefaultFactory
    {
        T Get<T>() where T : new();
        T Get<T>(String name) where T : new();
        void Init<T>(Func<IQueryable<T>, IQueryable<T>> query) where T : new();
        void Init<T>(String name, Func<IQueryable<T>, IQueryable<T>> query) where T : new();
    }
}