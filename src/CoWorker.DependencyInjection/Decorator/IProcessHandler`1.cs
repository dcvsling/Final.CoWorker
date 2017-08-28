using CoWorker.DependencyInjection.Factory;
using System.Security.Cryptography.X509Certificates;
using CoWorker.DependencyInjection.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using System;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoWorker.DependencyInjection.Decorator
{
    public interface IProcessHandler<TCommand> : ICommandHandler<TCommand>,IQueryHandler<TCommand> where TCommand : class
    {
        //void Execute(TCommand cmd);
        //IEnumerable<TResult> Query<TResult>(IQueryable<TCommand> query);
    }
}
