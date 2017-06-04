using AppChat.IMApi.AutoFac;
using AppChat.IMApi.Filters;
using Autofac;
using Autofac.Integration.WebApi;
using Serilog;
using System.Reflection;
using System.Web.Http;

namespace AppChat.IMApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            var builder = new ContainerBuilder();

            // 获取 HttpConfiguration.
            var config = GlobalConfiguration.Configuration;

            // 注册 Modules.
            builder.RegisterModule<LogModule>();
            builder.RegisterModule<ApiModule>();

            // 注册 Web API controllers.
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.Register(c => new ExceptionFilter(c.ResolveKeyed<ILogger>("webapi_exception"))).AsWebApiExceptionFilterFor<ApiController>().InstancePerLifetimeScope();

            // OPTIONAL: Register the Autofac filter provider.
            builder.RegisterWebApiFilterProvider(config);

            // Set the dependency resolver to be Autofac.
            var container = builder.Build();

            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}
