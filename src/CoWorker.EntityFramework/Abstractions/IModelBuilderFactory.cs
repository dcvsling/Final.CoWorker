using System.Linq;

namespace CoWorker.EntityFramework.Abstractions
{
    using Microsoft.EntityFrameworkCore;
    using System;
    public interface IModelBuilderFactory
    {
        ModelBuilder Create(Type type = null);
    }
}
