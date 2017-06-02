
using AppChat.DI;
using Autofac;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin.Cors;
using Owin;
using System.Diagnostics;

namespace AppChat.SignalRHostSelf
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseErrorPage();

            app.Map("/signalr", map =>
            {
                var config = new HubConfiguration
                {
                    EnableJSONP = true
                };

                map.UseCors(CorsOptions.AllowAll)
                   .RunSignalR(config);
            });

            GlobalHost.TraceManager.Switch.Level = SourceLevels.Information;

            #region 依赖注入区域
            var builder = new ContainerBuilder();

            builder.RegisterModule<CacheModule>();
            builder.RegisterModule<ServiceModule>();
            builder.RegisterModule<ElasticModule>();
            builder.RegisterModule<OrmModule>();

            builder.Build();
            #endregion
        }
    }
}
