using AppChat.ElasticSearch.Model;
using AppChat.ElasticSearch.Models;
using AppChat.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppChat.Service._Interface
{
    public interface IUserService
    {
        Task<JsonResultModel> GetChatRoomBaseInfo(int userid);
        bool IndexUserInfo(LayImUser user);
        JsonResultModel SearchLayImUsers(string keyword, int pageIndex = 1, int pageSize = 50);
        JsonResultModel SearchHistoryMsg(string groupId, DateTime? starttime = null, DateTime? endtime = null, string keyword = null, bool isfile = false, bool isimg = false, int pageIndex = 1, int pageSize = 20);
    }
}
