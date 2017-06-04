using AppChat.Facade.LogExt;
using AppChat.Utils.Config;
using Autofac;
using MassTransit;
using MassTransit.Pipeline;
using Serilog;
using System;
using System.Threading.Tasks;

namespace AppChat.Facade.AutoFac
{
    public class FacadeModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // Register Serilog and get it log to Seq and file.
            builder.Register<ILogger>((c, p) =>
            {
                var seqSinkServerAddress = AppSettingConfig.SeqSinkServerAddress;

                return new LoggerConfiguration().WriteTo.Seq(seqSinkServerAddress)
                                                .CreateLogger();
            }).SingleInstance();

            // Register all the consumers.
            builder.RegisterConsumers(System.Reflection.Assembly.GetExecutingAssembly());

            // Register the bus.
            builder.Register(context =>
            {
                var busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
                {
                    var uri = AppSettingConfig.RabbitMqBaseUri;

                    var host = cfg.Host(new Uri(uri), x =>
                    {
                        x.Username(AppSettingConfig.RabbitMqUserName);
                        x.Password(AppSettingConfig.RabbitMqUserPassword);
                    });

                    cfg.ReceiveEndpoint(host, AppSettingConfig.BaseQueueName + "_service", endPoint =>
                    {
                        // This will defer send/ publish until after the consumer completes successfully in this end point.
                        endPoint.UseExceptionLogger(context.Resolve<ILogger>());

                        endPoint.LoadFrom(context);
                    });
                });

                busControl.ConnectConsumeObserver(new ConsumeObserver());

                return busControl;
            })
            .SingleInstance()
            .As<IBusControl>()
            .As<IBus>();
        }
    }

    public class ConsumeObserver : IConsumeObserver
    {
        public Task ConsumeFault<T>(MassTransit.ConsumeContext<T> context, Exception exception) where T : class
        {
            return Task.FromResult<object>(null);
        }

        public Task PostConsume<T>(MassTransit.ConsumeContext<T> context) where T : class
        {
            return Task.FromResult<object>(null);
        }

        public Task PreConsume<T>(MassTransit.ConsumeContext<T> context) where T : class
        {
            return Task.FromResult<object>(null);
        }
    }
}
