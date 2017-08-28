using Microsoft.Extensions.Options;
using System;

namespace CoWorker.Infrastructure.Cache
{
    public interface ICache<TOptions> : IOptionsMonitorCache<TOptions> where TOptions : class
    {
        TOptions Get(String name = default);
    }
}