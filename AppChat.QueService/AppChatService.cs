using AppChat.Cache.AutoFac;
using AppChat.ElasticSearch.AutoFac;
using AppChat.Facade.AutoFac;
using AppChat.Service.AutoFac;
using Autofac;
using MassTransit;
using SqlSugar.AutoFac;

namespace AppChat.QueService
{
    public class AppChatService
    {
        private IContainer _container;

        public AppChatService()
        {
            // Dependency Registrer
            var builder = new ContainerBuilder();

            // Register Module.
            builder.RegisterModule<FacadeModule>();
            builder.RegisterModule<ServiceModule>();
            builder.RegisterModule<CacheModule>();
            builder.RegisterModule<ElasticModule>();
            builder.RegisterModule<OrmModule>();

            var container = builder.Build();

            _container = container;
        }

        public void Start()
        {
            _container.Resolve<IBusControl>().Start();
        }

        public void Stop()
        {
            _container.Resolve<IBusControl>().Stop();
        }
    }
}
