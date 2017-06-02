using AppChat.Model.Core;
using AppChat.Model.Online;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace AppChat.Cache
{
    public interface IRedisCache
    {
        Task CacheUserAfterLogin(int userid);
        string GetCurrentUserId(HttpContextBase contextBase = null);
        Task OperateOnlineUser(OnlineUser user, bool isDelete = false);
        bool IsOnline(int userid);
        Task SetUserFriendList(int userId, List<v_layim_friend_group_detail_info> list);
        Task<List<v_layim_friend_group_detail_info>> GetUserFriendList(int userId);
    }
}
