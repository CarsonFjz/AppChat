using AppChat.Model;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppChat.Utils.JsonResult
{
    public static class JsonResultHelper
    {
        public static JsonResultModel CreateJson(bool success = true,string msg = "")
        {
            return new JsonResultModel { code = success ? JsonResultType.Success : JsonResultType.Failed, data = null, msg = msg };
        }
        public static JsonResultModel CreateJson(object data, bool success = true, string msg = "")
        {
            return new JsonResultModel { code = success ? JsonResultType.Success : JsonResultType.Failed, data = data, msg = msg };
        }


        //异步方法
        public static async Task<JsonResultModel> CreateJsonAsync(bool success = true, string msg = "")
        {
            return await Task.Run(() =>
            {
                return new JsonResultModel { code = success ? JsonResultType.Success : JsonResultType.Failed, data = null, msg = msg };
            });
        }
        public static async Task<JsonResultModel> CreateJsonAsync(object data, bool success = true, string msg = "")
        {
            return await Task.Run(() =>
            {
                return new JsonResultModel { code = success ? JsonResultType.Success : JsonResultType.Failed, data = data, msg = msg };
            });
        }
    }
}
