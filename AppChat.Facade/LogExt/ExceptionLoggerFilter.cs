using System;
using Serilog;
using System.Threading.Tasks;
using MassTransit;
using MassTransit.Pipeline;
using System.Threading;

namespace AppChat.Facade.LogExt
{
    internal class ExceptionLoggerFilter<T> : IFilter<T> where T : class, PipeContext
    {
        private readonly ILogger _logger;

        long _exceptionCount;
        long _successCount;
        long _attemptCount;

        public ExceptionLoggerFilter(Serilog.ILogger logger)
        {
            _logger = logger;
        }

        public void Probe(ProbeContext context)
        {
            var scope = context.CreateFilterScope("exceptionLogger");

            scope.Add("attempted", _attemptCount);
            scope.Add("succeeded", _successCount);
            scope.Add("faulted", _exceptionCount);
        }

        public async Task Send(T context, IPipe<T> next)
        {
            try
            {
                Interlocked.Increment(ref _attemptCount);

                await next.Send(context);

                Interlocked.Increment(ref _successCount);
            }
            catch (Exception ex)
            {
                Interlocked.Increment(ref _exceptionCount);

                _logger.Error(ex, String.Format("An exception occurred: {0}", ex.Message));

                // propagate the exception up the call stack
                throw;
            }
        }
    }
}