using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore.Design;

namespace CoWorker.Builder
{
    using Microsoft.Extensions.DependencyInjection;
    using CoWorker.EntityFramework.Abstractions;
    using CoWorker.EntityFramework;

    public class DefaultLoggingOperationReportHandler : IOperationReportHandler
    {
        private readonly ILogger<IOperationReportHandler> _logger;

        public DefaultLoggingOperationReportHandler(ILogger<IOperationReportHandler> logger)
        {
            this._logger = logger;
        }
        public System.Int32 Version => 1;

        public void OnError(System.String message)
            => _logger.LogError(message);
        public void OnInformation(System.String message)
            => _logger.LogInformation(message);
        public void OnVerbose(System.String message)
            => _logger.LogTrace(message);
        public void OnWarning(System.String message)
            => _logger.LogWarning(message);
    }
}