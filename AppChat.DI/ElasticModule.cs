using AppChat.ElasticSearch.Core;
using AppChat.ElasticSearch.Models;
using Autofac;

namespace AppChat.DI
{
    public class ElasticModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(Elastic<>)).As(typeof(IElastic<>)).SingleInstance();

            using (IContainer container = builder.Build())
            {
                container.Resolve<IElastic<LayImUser>>();
                container.Resolve<IElastic<LayImGroup>>();
            }
        }
    }
}
