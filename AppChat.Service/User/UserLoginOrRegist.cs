using AppChat.Cache;
using AppChat.Model;
using AppChat.Model.Convert;
using AppChat.Model.Core;
using AppChat.Service._Interface;
using AppChat.Utils.JsonResult;
using SqlSugar;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppChat.Service.User
{
    public class UserLoginOrRegist : IUserLoginOrRegist
    {
        private IRedisCache _redisCache;
        private IElasticGroupService _elastic;
        private SqlSugarClient _context;
        public UserLoginOrRegist(IRedisCache redisCache, SqlSugarClient context)//,IElasticGroupService elastic
        {
            _redisCache = redisCache;
            //_elastic = elastic;
            _context = context;
        }

        #region 用户登录流程
        /// <summary>
        /// 用户登陆或者注册，返回用户id如果为 0 说明密码错误
        /// </summary>
        /// <param name="loginName"></param>
        /// <param name="loginPwd"></param>
        /// <param name="nickName"></param>
        /// <returns></returns>
        public JsonResultModel UserLogin(string loginName, string loginPwd, out int userid)
        {
            userid = 0;

            //TODO:判断用户是否存在,存在就获取信息 done
            var loginUser = _context.Queryable<layim_user>().Where(x => x.loginname == loginName && x.loginpwd == loginPwd).ToList();

            if (loginUser != null)
            {
                return JsonResultHelper.CreateJson(new { userid = loginUser });
            }
            return JsonResultHelper.CreateJson(false);

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
                var friendList = _context.Queryable<v_layim_friend_group_detail_info>().Where(x => x.userid == userid).ToList();

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
            var userMessageResult = _context.Queryable<v_layim_apply>()
                                            .Where(x => x.targetid == userid.ToString() || x.userid == userid)
                                            .OrderBy(x=>x.applytime,OrderByType.Desc)
                                            .ToList().Convert(userid);

            return await JsonResultHelper.CreateJsonAsync(userMessageResult, true);
        }
        #endregion

        #region 读取用户所在的群
        public async Task<JsonResultModel> GetUserAllGroups(int userId)
        {
            var userGroupResult = _context.Queryable<layim_friend_group_detail>().Where(x => x.uid == userId).ToList();

            return await JsonResultHelper.CreateJsonAsync(userGroupResult, true);
        }
        #endregion

    }
}
