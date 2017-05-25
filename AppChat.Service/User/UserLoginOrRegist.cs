using AppChat.Cache;
using AppChat.Model;
using AppChat.Model.Core;
using AppChat.Model.Message;
using AppChat.Repository;
using AppChat.Service.Group;
using AppChat.Utils.Extension;
using AppChat.Utils.JsonResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppChat.Service.User
{
    public interface IUserLoginOrRegist
    {
    }
    public class UserLoginOrRegist : IUserLoginOrRegist
    {
        private IRedisCache _redisCache;
        private IElasticGroupService _elastic;
        private IBaseRepository<layim_user> _user;
        private IBaseRepository<ApplyMessage> _applyMessage;
        private IBaseRepository<layim_friend_group_detail> _friendGroup;
        private IBaseRepository<v_layim_friend_group_detail_info> _friendListInfo;
        public UserLoginOrRegist(IRedisCache redisCache,
                           IElasticGroupService elastic,
                           IBaseRepository<layim_user> user,
                           IBaseRepository<ApplyMessage> applyMessage,
                           IBaseRepository<layim_friend_group_detail> friendGroup,
                           IBaseRepository<v_layim_friend_group_detail_info> friendListInfo)
        {
            _redisCache = redisCache;
            _elastic = elastic;
            _user = user;
            _applyMessage = applyMessage;
            _friendListInfo = friendListInfo;
            _friendGroup = friendGroup;
        }


        //----------------------------------初始化用户登录star--------------------------------------

        #region 用户登录流程
        /// <summary>
        /// 用户登陆或者注册，返回用户id如果为 0 说明密码错误
        /// </summary>
        /// <param name="loginName"></param>
        /// <param name="loginPwd"></param>
        /// <param name="nickName"></param>
        /// <returns></returns>
        public Task<JsonResultModel> UserLoginOrRegister(string loginName, string loginPwd, out int userid)
        {
            userid = 0;
            var wrongNameOrPwdFlag = -1;
            if (string.IsNullOrEmpty(loginName) || string.IsNullOrEmpty(loginPwd))
            {
                return JsonResultHelper.CreateJsonAsync(new { userid = wrongNameOrPwdFlag });
            }
            //TODO:判断用户是否存在,存在就获取信息 done
            var loginUser = _user.QuerySingle(x => x.loginname == loginName && x.loginpwd == loginPwd);

            if (loginUser != null)
            {
                return JsonResultHelper.CreateJsonAsync(new { userid = loginUser.id });
            }
            return JsonResultHelper.CreateJsonAsync(false);

        }
        #endregion

        #region 获取某个用户的好友列表
        /// <summary>
        /// 获取某个用户的好友列表
        /// </summary>
        /// <param name="userid">用户ID</param>
        /// <returns>返回格式如下 ""或者 "10001,10002,10003"</returns>
        public async Task<List<v_layim_friend_group_detail_info>> GetUserFriends(int userid)
        {
            //先读取缓存
            var friends = await _redisCache.GetUserFriendList(userid);
            //如果缓存中没有
            if (friends.Count == 0)
            {
                //从数据库读取，在保存到缓存中
                var friendList = _friendListInfo.QueryByWhere(x => x.userid == userid);

                StringBuilder friendStr = new StringBuilder();

                foreach (var item in friendList)
                {
                    friendStr.Append(item.userid);
                }
                await _redisCache.SetUserFriendList(userid, friends);
            }
            return friends;
        }
        #endregion

        #region 获取用户有关的消息
        public async Task<JsonResultModel> GetUserApplyMessage(int userid)
        {
            string procName = "exec Proc_LayIM_GetUserApplyRecord @userid";

            var userMessageResult = await _applyMessage.QueryBySqlAsync<ApplyMessage>(procName, new { userid = userid });

            foreach (var item in userMessageResult)
            {
                item.isself = userid == item.userid;
                item.addtime = item.addtime.ToDateTime().ToString("yyyy/MM/dd HH:mm");
            }

            return await JsonResultHelper.CreateJsonAsync(userMessageResult, true);
        }
        #endregion

        #region 读取用户所在的群
        public Task<List<layim_friend_group_detail>> GetUserAllGroups(string userId)
        {
            return _friendGroup.QueryByWhereAsync(x => x.uid == Convert.ToInt32(userId));
        }
        #endregion

        //----------------------------------初始化用户登录end--------------------------------------
    }
}
