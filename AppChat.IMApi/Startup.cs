
using Microsoft.Owin;
using Owin;
using AppChat.SignalR.UserIdProvider;
using Microsoft.AspNet.SignalR;
using System.Diagnostics;
using System.Web.Cors;
using Microsoft.Owin.Cors;
using System.Threading.Tasks;
using System;
using AppChat.SignalR.Hubs;

[assembly: OwinStartup(typeof(AppChat.Web.Startup))]

namespace AppChat.Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var userIdProvider = new UserCache();

            GlobalHost.DependencyResolver.Register(typeof(IUserIdProvider), () => userIdProvider);

            app.Map("/signalR", map =>
            {
                map.UseCors(CorsOptions.AllowAll);

                var config = new HubConfiguration
                {
                    EnableJSONP = true,
                    EnableJavaScriptProxies = true
                };

                map.RunSignalR(config);
            });
        }

        //private static readonly Lazy<CorsOptions> SignalrCorsOptions = new Lazy<CorsOptions>(() =>
        //{
        //    return new CorsOptions
        //    {
        //        PolicyProvider = new CorsPolicyProvider
        //        {
        //            PolicyResolver = context =>
        //            {
        //                var policy = new CorsPolicy();
        //                policy.AllowAnyOrigin = true;
        //                policy.AllowAnyMethod = true;
        //                policy.AllowAnyHeader = true;
        //                policy.SupportsCredentials = false;
        //                return Task.FromResult(policy);
        //            }
        //        }
        //    };
        //});
    }
}
