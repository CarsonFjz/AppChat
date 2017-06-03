using AppChat.Model.Core;
using AppChat.Model.Online;
using AppChat.Utils.Consts;
using AppChat.Utils.Cookie;
using AppChat.Utils.Random;
using StackExchange.Redis.Extensions.Core;
using StackExchange.Redis.Extensions.Newtonsoft;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace AppChat.Cache
{
    public class RedisCache : IRedisCache
    {
        static NewtonsoftSerializer serializer = new NewtonsoftSerializer();
        StackExchangeRedisCacheClient cacheClient = new StackExchangeRedisCacheClient(serializer);

        #region 缓存用户的token
        public async Task CacheUserAfterLogin(int userid)
        {
            var key = LayIMConst.LayIM_Cache_UserLoginToken;
            var token = RandomHelper.GetUserToken();
            //存redis
            bool result = await cacheClient.HashSetAsync(key, token, userid);
            if (result)
            {
                //写cookie
                CookieHelper.SetCookie(key, token);
            }
            else
            {
                //记录日志
            }
        }
        #endregion

        #region 获取当前登录用户的用户id

        public string GetCurrentUserId(HttpContextBase contextBase = null)
        {
            var key = LayIMConst.LayIM_Cache_UserLoginToken;
            string token = CookieHelper.GetCookieValue(key, contextBase);
            if (token == string.Empty)
            {
                return TipsConst.cookieIsEmpty;
            }
            return cacheClient.HashGet<string>(key, token);
        }
        #endregion

        #region 在线用户处理
        public Task OperateOnlineUser(OnlineUser user, bool isDelete = false)
        {
            if (isDelete)
            {
                return cacheClient.HashDeleteAsync(LayIMConst.LayIM_All_OnlineUsers, user.userid);
            }
            else
            {
                return cacheClient.HashSetAsync(LayIMConst.LayIM_All_OnlineUsers, user.userid, user.connectionid);
            }
        }
        #endregion

        #region 根据用户ID判断某个用户是否在线

        public bool IsOnline(int userid)
        {
            string result = cacheClient.HashGet<string>(LayIMConst.LayIM_All_OnlineUsers, userid.ToString());

            return !string.IsNullOrEmpty(result);
        }
        #endregion

        #region 缓存用户好友列表
        /// <summary>
        /// 缓存用户好友列表
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="list">好友列表 1,2,3,4 （不知道好友列表长度会不会超过限制，如果超过限制，就不能用string存储了）</param>
        /// <returns>返回是否成功 true  false</returns>
        public Task SetUserFriendList(int userId, List<v_layim_friend_group_detail_info> list)
        {
            if (list.Count > 0)
            {
                //用户好友列表key
                var key = string.Format(LayIMConst.LayIM_All_UserFriends, userId);
                //如果key已经存在，先remove掉
                if (cacheClient.Exists(key))
                {
                    cacheClient.Remove(key);
                }
                //一天过期
                return cacheClient.AddAsync(key, list, DateTimeOffset.Now.AddDays(1));
            }
            return null;
        }
        #endregion

        #region 获取用户好友列表
        public async Task<List<v_layim_friend_group_detail_info>> GetUserFriendList(int userId)
        {
            //用户好友列表key
            var key = string.Format(LayIMConst.LayIM_All_UserFriends, userId);
            //一天过期
            //TODO:存储一天会不会有刷新问题, 应该在用户操作好友删除或者增加时候同时更新缓存的数据
            return await cacheClient.GetAsync<List<v_layim_friend_group_detail_info>>(key);
        }
        #endregion
    }
}
