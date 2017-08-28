using CoWorker.Abstractions;
using CoWorker.Rest.Internal;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Routing;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoWorker.Rest.Features
{
    public abstract class NamingFeature<T>
    {
        public IDictionary<string, T> Features { get; } = new Dictionary<string,T>();
    }
}
