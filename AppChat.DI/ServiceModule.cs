using AppChat.Service;
using AppChat.Service._Interface;
using AppChat.Service.File;
using AppChat.Service.User;
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
            builder.RegisterType<CreateGroupService>().As<ICreateGroupService>().InstancePerLifetimeScope();
            builder.RegisterType<FileUploadService>().As<IFileUploadService>().InstancePerLifetimeScope();
            builder.RegisterType<UserLoginOrRegist>().As<IUserLoginOrRegist>().InstancePerLifetimeScope();
            builder.RegisterType<UserService>().As<IUserService>().InstancePerLifetimeScope();
        }
    }
}
