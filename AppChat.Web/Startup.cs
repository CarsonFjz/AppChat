using AppChat.Web.UserIdProvider;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Owin;
[assembly: OwinStartup(typeof(Startup))]
public partial class Startup
{
    public void Configuration(IAppBuilder app)
    {
        var userIdProvider = new UserCache();//自定义用户方法

        GlobalHost.DependencyResolver.Register(typeof(IUserIdProvider), () => userIdProvider);

        app.Map("/layim", map =>
        {
            var hubConfiguration = new HubConfiguration()
            {
                EnableJSONP = true,
                EnableJavaScriptProxies = true
            };
            map.RunSignalR(hubConfiguration);
        });
    }
}