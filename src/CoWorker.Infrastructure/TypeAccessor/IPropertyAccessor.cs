using System;
using System.Linq.Expressions;

namespace CoWorker.Infrastructure.TypeAccessor
{
    public interface IPropertyAccessor
    {
        Type DeclareType { get; }
        string PropertyName { get; }
        object Get(object obj);
        void Set(object obj, object val);
    }
}