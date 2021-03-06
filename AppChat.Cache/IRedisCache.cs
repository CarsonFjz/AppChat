﻿using AppChat.Model.Core;
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
        bool CacheUserAfterLogin(Guid userid, string key = null, string token = null);
        string GetCurrentUserId(HttpContextBase contextBase = null);
        Task OperateOnlineUser(OnlineUser user, bool isDelete = false);
        bool IsOnline(Guid userid);
        Task SetUserFriendList(Guid userid, List<v_layim_friend_group_detail_info> list);
        Task<List<v_layim_friend_group_detail_info>> GetUserFriendList(Guid userid);
    }
}
