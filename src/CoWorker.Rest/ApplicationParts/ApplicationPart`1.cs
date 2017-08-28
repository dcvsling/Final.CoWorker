
namespace CoWorker.Rest.ApplicationParts
{
    using CoWorker.Infrastructure.TypeStore;
    using System;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc.ApplicationParts;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Options;

    public class ApplicationPart<T> : ApplicationPart
    {
        public ApplicationPart(T t)
        {
            Value = t;
        }
        public override String Name => typeof(T).Name;
        public T Value { get; }
    }
}
