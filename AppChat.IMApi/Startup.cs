using AppChat.IMApi.UserIdProvider;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Owin;
using System.Diagnostics;

[assembly: OwinStartup(typeof(Startup))]
public partial class Startup
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
    }
}