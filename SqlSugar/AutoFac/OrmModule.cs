using AppChat.Utils.Config;
using Autofac;

namespace SqlSugar.AutoFac
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
