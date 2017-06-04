using AppChat.Utils.Config;
using Autofac;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppChat.IMApi.AutoFac
{
    public class LogModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register<ILogger>((c, p) =>
            {
                var seqSinkServerAddress = AppSettingConfig.SeqSinkServerAddress;

                return new LoggerConfiguration().WriteTo.Seq(seqSinkServerAddress)
                                                .CreateLogger();
            })
            .Named<ILogger>("webapi_exception")
            .SingleInstance();
        }
    }
}
