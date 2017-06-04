using MassTransit;
using Serilog;


namespace AppChat.Facade.LogExt
{
    public static class ExceptionLoggerExtension
    {
        public static void UseExceptionLogger<T>(this IPipeConfigurator<T> configurator, ILogger logger) where T : class, PipeContext
        {
            configurator.AddPipeSpecification(new ExceptionLoggerSpecification<T>(logger));
        }
    }
}
