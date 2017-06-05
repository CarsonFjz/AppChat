using AppChat.Cache;
using AppChat.Utils.Consts;
using AppChat.Utils.Cookie;
using AppChat.Utils.Single;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppChat.Web.UserIdProvider
{
    public class UserCache : IUserIdProvider
    {
        /// <summary>
        /// 自定义获取用户ID方法
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public string GetUserId(IRequest request)
        {
            //直接读取Cookie中的userid，然后将userid返回，否则返回空，未登录
            return new RedisCache().GetCurrentUserId();
        }
    }
}