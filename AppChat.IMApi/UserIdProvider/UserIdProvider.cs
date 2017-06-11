using AppChat.Cache;
using Microsoft.AspNet.SignalR;

namespace AppChat.IMApi.UserIdProvider
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