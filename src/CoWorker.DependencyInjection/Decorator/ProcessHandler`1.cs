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

    public class ProcessHandler<TCommand> : IProcessHandler<TCommand>
        where TCommand : class
    {
        private readonly ICommandHandler<TCommand> _command;
        private readonly IQueryHandler<TCommand> _query;

        public ProcessHandler(
            ICommandHandler<TCommand> command,
            IQueryHandler<TCommand> query)
        {
            this._command = command;
            this._query = query;
        }
        public void Handle(TCommand cmd) => _command.Handle(cmd);
        public IEnumerable<TResult> Handle<TResult>(Func<IQueryable<TCommand>, IEnumerable<TResult>> query)
            => _query.Handle(query);
    }
}
