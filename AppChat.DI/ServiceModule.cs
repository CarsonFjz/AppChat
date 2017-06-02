using AppChat.Service;
using AppChat.Service._Interface;
using AppChat.Service.File;
using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppChat.DI
{
    public class ServiceModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CreateGroupService>().As<ICreateGroupService>().SingleInstance();
            builder.RegisterType<FileUploadService>().As<IFileUploadService>().SingleInstance();
        }
    }
}
