using AppChat.Utils.Extension;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AppChat.Utils.Config
{
    public class AppSettingConfig
    {
        private static string AppSettingValue([CallerMemberName]string key = null)
        {
            return ConfigurationManager.AppSettings[key];
        }

        #region ElasticConfig
        public static string ElasticHost
        {
            get
            {
                return AppSettingValue();
            }
        }

        public static int ElasticPort
        {
            get
            {
                return AppSettingValue().ToInt();
            }
        }

        public static int ElasticTimeOut
        {
            get
            {
                return 600 * 1000;
            }
        }
        #endregion

        #region Sql
        public static string SqlConnection
        {
            get
            {
                return AppSettingValue();
            }
        }
        #endregion
    }
}
