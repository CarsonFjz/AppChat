using AppChat.Utils.Config;
using Autofac;
using MassTransit;
using MassTransit.Pipeline;
using System;
using System.Threading.Tasks;

namespace AppChat.IMApi.AutoFac
{
    public class ApiModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //队列发送端
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
                });

                busControl.ConnectSendObserver(new SendObserver());

                busControl.Start();

                return busControl;
            })
            .SingleInstance()
            .As<IBusControl>()
            .As<IBus>();

            builder.RegisterGeneric(typeof(RequestClient<,>)).AsSelf().AsImplementedInterfaces();
        }

        public class RequestClient<TRequest, TResponse> : MessageRequestClient<TRequest, TResponse>
            where TRequest : class
            where TResponse : class
        {
            public RequestClient(IBus bus) : base(bus, new Uri(AppSettingConfig.RabbitMqBaseUri + AppSettingConfig.BaseQueueName + "_service"), TimeSpan.FromSeconds(60))
            {
            }
        }
    }

    public class SendObserver : ISendObserver
    {
        public Task PostSend<T>(SendContext<T> context) where T : class
        {
            return Task.FromResult<object>(null);
        }

        public Task PreSend<T>(SendContext<T> context) where T : class
        {
            return Task.FromResult<object>(null);
        }

        public Task SendFault<T>(SendContext<T> context, Exception exception) where T : class
        {
            return Task.FromResult<object>(null);
        }
    }
}
