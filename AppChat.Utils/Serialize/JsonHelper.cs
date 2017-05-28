using Newtonsoft.Json;

namespace AppChat.Utils.Serialize
{
    public sealed class JsonHelper
    {
        #region 序列化
        public static string SerializeObject(object value)
        {
            return JsonConvert.SerializeObject(value);
        }
        #endregion

        #region 反序列化
        public static T DeserializeObject<T>(string value)
        {
            return JsonConvert.DeserializeObject<T>(value);
        }
        #endregion
    }
}
