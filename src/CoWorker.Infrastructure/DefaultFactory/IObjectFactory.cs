using System;
using System.Linq;

namespace CoWorker.Infrastructure.DefaultFactory
{
    public interface IObjectFactory
    {
        object Create(Type type);
    }
}