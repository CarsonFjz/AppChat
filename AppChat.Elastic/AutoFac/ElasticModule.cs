using AppChat.ElasticSearch.Core;
using AppChat.ElasticSearch.Models;
using Autofac;

namespace AppChat.ElasticSearch.AutoFac
{
    public class ElasticModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //builder.RegisterGeneric(typeof(Elastic<>)).As(typeof(Elastic<>)).SingleInstance();

            //using (IContainer container = builder.Build())
            //{
            //    container.Resolve<IElastic<LayImUser>>();
            //    container.Resolve<IElastic<LayImGroup>>();
            //}
        }
    }
}
