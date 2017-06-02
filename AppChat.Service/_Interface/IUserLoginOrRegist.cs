using AppChat.Model;
using AppChat.Model.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppChat.Service._Interface
{
    public interface IUserLoginOrRegist
    {

        #region 用户注册或者登陆
        JsonResultModel UserLoginOrRegister(string loginName, string loginPwd, out int userid);
        Task<List<v_layim_friend_group_detail_info>> GetUserFriends(int userid);
        Task<JsonResultModel> GetUserApplyMessage(int userid);
        Task<JsonResultModel> GetUserAllGroups(int userId); 
        #endregion


    }
}
