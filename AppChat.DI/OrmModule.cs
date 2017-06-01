using AppChat.ORM;
using AppChat.Utils.Config;
using Autofac;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppChat.DI
{
    public class OrmModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(context=> 
            {
                var connection = new ConnectionConfig()
                {
                    ConnectionString = AppSettingConfig.SqlConnection,
                    DbType = DbType.SqlServer
                };

                return new SqlSugarClient(connection);
            });
        }
    }
}
