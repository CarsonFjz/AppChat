﻿using AppChat.ElasticSearch.Models;
using AppChat.Model;
using AppChat.Model.Core;
using AppChat.Model.Message;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppChat.Service._Interface
{
    public interface IUserService
    {
        BaseListResult GetChatRoomBaseInfo(Guid userid);
        bool IndexUserInfo(LayImUser user);
        JsonResultModel SearchLayImUsers(string keyword, int pageIndex = 1, int pageSize = 50);
        JsonResultModel SearchHistoryMsg(string groupId, DateTime? starttime = null, DateTime? endtime = null, string keyword = null, bool isfile = false, bool isimg = false, int pageIndex = 1, int pageSize = 20);
        Task<List<v_layim_friend_group_detail_info>> GetUserFriends(Guid userid);
        List<ApplyMessage> GetUserApplyMessage(Guid userid);
        List<layim_friend_group_detail> GetUserAllGroups(Guid userid);
    }
}
