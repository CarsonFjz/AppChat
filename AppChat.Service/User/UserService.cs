using AppChat.Cache;
using AppChat.ElasticSearch.Core;
using AppChat.ElasticSearch.Ext;
using AppChat.ElasticSearch.Model;
using AppChat.ElasticSearch.Models;
using AppChat.Model;
using AppChat.Model.Convert;
using AppChat.Model.Core;
using AppChat.Model.Message;
using AppChat.Service._Interface;
using AppChat.Utils.Extension;
using AppChat.Utils.JsonResult;
using AppChat.Utils.Validate;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppChat.Service.User
{
    public class UserService : IUserService
    {
        private IRedisCache _redisCacheService;
        private IElastic<LayImUser> _elasticUserService;
        private IElasticChat _elasticChatService = new ElasticChat();
        private SqlSugarClient _context;
        public UserService(IRedisCache redisCacheService, IElastic<LayImUser> elasticService, IElasticChat elasticChatService, SqlSugarClient context)
        {
            _redisCacheService = redisCacheService;
            _elasticUserService = elasticService;
            _elasticChatService = elasticChatService;
            _context = context;

            //设置索引关键词
            _elasticUserService.SetIndexInfo("layim", "layim_user");
            _elasticChatService.SetIndexInfo("layim", "chatinfo");
        }

        #region 获取用户登录聊天室后的基本信息 
        public BaseListResult GetChatRoomBaseInfo(int userid)
        {
            if (userid == 0) { throw new ArgumentException("userid can't be zero"); }

            #region 1.0 当前用户信息
            var mine = _context.Queryable<layim_user>().Where(x => x.id == userid).Single();

            var mineModel = new UserEntity
            {
                id = mine.id,
                avatar = mine.headphoto,
                sign = mine.sign,
                username = mine.nickname,
                status = "online"
            };
            #endregion

            #region 2.0 好友信息
            //2.1 获取好友用户组
            var friendGroup = _context.Queryable<v_layim_friend_group>().Where(x => x.userid == userid).OrderBy(x => x.sort).ToList();
            //2.2 把用户组id取出来
            var friendGroupIdList = friendGroup.Select(x => x.id);
            //2.3 找出每个用户组下所有好友信息
            var friendList = _context.Queryable<v_layim_friend_group_detail>().In<Guid>(x => x.gid).ToList();

            var friendGroupModel = friendGroup.Select(x => new FriendGroupEntity()
            {
                id = x.id,
                groupname = x.name,
                online = 0,
                list = friendList.Select(y => new GroupUserEntity()
                {
                    id = y.uid,
                    avatar = y.headphoto,
                    groupid = y.gid,
                    remarkname = y.gname,
                    username = y.nickname,
                    sign = y.sign,
                    //status之前的字段是为空的，现在我们把他的在线状态加上，IsOnline方法接收一个userid参数，从Redis缓存中读取该用户是否在线并返回
                    status = _redisCacheService.IsOnline(y.uid) ? "online" : "hide"
                }).OrderByDescending(y => y.status)
            });

            #endregion

            #region 3.0 群信息
            var groupModel = _context.Queryable<v_group_detail, layim_group>((d, g) => new object[] { JoinType.Left, d.gid == g.id })
                                .Where((d, g) => d.userid == userid && g.status == 0)
                                .Select((d, g) => new GroupEntity {
                                    id = g.id,
                                    groupname = g.name,
                                    avatar = g.headphoto,
                                    groupdesc = g.groupdesc
                                }).ToList();
            #endregion

            BaseListResult result = new BaseListResult
            {
                mine = mineModel,
                friend = friendGroupModel,
                group = groupModel
            };

            return result;
        }
        #endregion

        #region 获取用户初始化信息
        /// <summary>
        /// 获取某个用户的好友列表
        /// </summary>
        /// <param name="userid">用户ID</param>
        /// <returns>返回格式如下 ""或者 "10001,10002,10003"</returns>
        public async Task<List<v_layim_friend_group_detail_info>> GetUserFriends(int userid)
        {
            //先读取缓存
            var friends = await _redisCacheService.GetUserFriendList(userid);
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
                await _redisCacheService.SetUserFriendList(userid, friends);
            }
            return friends;
        }

        /// <summary>
        /// 获取用户相关消息
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public List<ApplyMessage> GetUserApplyMessage(int userid)
        {
            return _context.Queryable<v_layim_apply>()
                           .Where(x => x.targetid == userid.ToString() || x.userid == userid)
                           .OrderBy(x => x.applytime, OrderByType.Desc)
                           .ToList().Convert(userid);
        }

        /// <summary>
        /// 读取用户所在群
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<layim_friend_group_detail> GetUserAllGroups(int userId)
        {
            return _context.Queryable<layim_friend_group_detail>().Where(x => x.uid == userId).ToList();
        }
        #endregion

        //#region 获取群组人员信息
        //public JsonResultModel GetGroupMembers(int groupid)
        //{
        //    var ds = _dal.GetGroupMembers(groupid);
        //    if (ds != null)
        //    {
        //        var rowOwner = ds.Tables[0].Rows[0];
        //        MembersListResult result = new MembersListResult
        //        {
        //            owner = new UserEntity
        //            {
        //                id = rowOwner["userid"].ToInt(),
        //                avatar = rowOwner["avatar"].ToString(),
        //                username = rowOwner["username"].ToString(),
        //                sign = rowOwner["sign"].ToString(),
        //            },
        //            list = ds.Tables[1].Rows.Cast<DataRow>().Select(x => new GroupUserEntity
        //            {
        //                id = x["userid"].ToInt(),
        //                avatar = x["avatar"].ToString(),
        //                groupid = groupid,
        //                remarkname = x["remarkname"].ToString(),
        //                sign = x["sign"].ToString(),
        //                username = x["username"].ToString()
        //            })
        //        };
        //        return JsonResultHelper.CreateJson(result);
        //    }
        //    return JsonResultHelper.CreateJson(null, false);
        //}
        //#endregion

        //#region 用户创建群
        //public JsonResultModel CreateGroup(string groupName, string groupDesc, int userid)
        //{
        //    var dt = _dal.CreateGroup(groupName, groupDesc, userid);
        //    if (dt != null && dt.Rows.Count == 1)
        //    {
        //        //同步ES库

        //        var group = _elasticService.IndexGroup(dt);
        //        var data = GetMessage(group);
        //        return JsonResultHelper.CreateJson(data, true);
        //    }
        //    else
        //    {
        //        return JsonResultHelper.CreateJson(null, false);
        //    }
        //}

        //private UserGroupCreatedMessage GetMessage(LayImGroup group)
        //{
        //    return new UserGroupCreatedMessage
        //    {
        //        id = group.id.ToInt(),
        //        avatar = group.avatar,
        //        groupdesc = group.groupdesc,
        //        groupname = group.groupname,
        //        memebers = group.allcount,
        //        type = LayIMConst.LayIMGroupType
        //    };
        //}
        //#endregion

        //#region 更换用户皮肤
        //public bool UpdateUserSkin(int userid, string path)
        //{
        //    return _dal.UpdateUserSkin(userid, path);
        //}
        //#endregion

        #region ES部分，此部分包含查询逻辑，搜索部分都是从ES中搜索，其他都是从SQL中出数据
        public bool IndexUserInfo(LayImUser user)
        {
            bool result = _elasticUserService.Index(user);
            return result;
        }

        /// <summary>
        /// 搜索用户
        /// </summary>
        /// <param name="keyword">可以是IM号，或者昵称，或者男女，或者是否在线（目前仅支持IM号和昵称搜索）</param>
        /// <returns></returns>
        public JsonResultModel SearchLayImUsers(string keyword, int pageIndex = 1, int pageSize = 50)
        {

            var result = SearchUser(keyword, pageIndex, pageSize);

            return JsonResultHelper.CreateJson(result);
        }

        private BaseQueryEntity<LayImUser> SearchUser(string keyword, int pageIndex = 1, int pageSize = 50)
        {
            var hasvalue = ValidateHelper.HasValue(keyword);
            var from = (pageIndex - 1) * pageSize;
            //全部的时候按照省份排序
            string queryAll = "{\"query\":{\"match_all\":{}},\"from\":" + from + ",\"size\":" + pageSize + ",\"sort\":{\"province\":{\"order\":\"asc\"}}}";
            //按照关键字搜索的时候，默认排序，会把最接近在在最上边
            int im = hasvalue ? keyword.ToInt() : 0;
            //这里增加im是否为int类型判断，如果是int类型，那么可能是查询用户的IM号码，否则就是关键字查询
            string term = im == 0 ? "{\"im\":0}" : "{\"im\":" + keyword + "}";
            string queryWithKeyWord = "{\"query\":{\"filtered\":{\"filter\":{\"or\":[{\"term\":" + term + "},{\"query\":{\"match_phrase\":{\"nickname\":{\"query\":\"" + keyword + "\",\"slop\":0}}}}]}}},\"from\":" + from + ",\"size\":" + pageSize + ",\"sort\":{}}";
            string queryFinal = hasvalue ? queryWithKeyWord : queryAll;
            var result = _elasticUserService.QueryBayConditions(queryFinal);
            return result;
        }


        #endregion

        #region ES部分，读取历史纪录 根据条件 开始时间，结束时间  聊天关键字  组

        public JsonResultModel SearchHistoryMsg(string groupId, DateTime? starttime = null, DateTime? endtime = null, string keyword = null, bool isfile = false, bool isimg = false, int pageIndex = 1, int pageSize = 20)
        {
            string st = starttime == null ? "" : starttime.Value.ToString("yyyy-MM-dd");
            string et = endtime == null ? "" : endtime.Value.ToString("yyyy-MM-dd");
            int from = (pageIndex - 1) * pageSize;
            //某个聊天组查询
            string queryGroup = "{\"query\": {\"match\": { \"roomid\": \"FRIEND_12686_10035\" }}}";
            //关键字查询
            string queryKeyWord = "{ \"query\": {\"match_phrase\": {\"content\": {\"query\": \"" + keyword + "\",\"slop\": 0} } }}";
            //是否图片 查询
            string queryImg = "{ \"term\": {\"isimg\": true }}";
            //是否包含文件查询
            string queryFile = "{ \"term\": {\"isfile\": true }}";
            //大于小于某个时间段查询
            string queryTimeRange = "{\"range\": {\"addtime\": { \"gt\": \"" + st + "\",\"lt\": \"" + et + "\" }} }";
            //大于某个时间
            string queryTimeRangeGt = "{\"range\": {\"addtime\": { \"gt\": \"" + st + "\"}} }";
            //小于某个时间
            string queryTimeRangeLt = "{\"range\": {\"addtime\": { \"lt\": \"" + et + "\" }} }";
            string queryAnd = queryGroup;
            if (starttime != null && endtime != null)
            {
                queryAnd += "," + queryTimeRange;
            }
            if (starttime != null)
            {
                queryAnd += "," + queryTimeRangeGt;
            }
            if (endtime != null)
            {
                queryAnd += "," + queryTimeRangeLt;
            }
            if (!string.IsNullOrEmpty(keyword))
            {
                queryAnd += "," + queryKeyWord;
            }
            if (isfile)
            {
                queryAnd += "," + queryFile;
            }
            if (isimg)
            {
                queryAnd += "," + queryImg;
            }
            //最终查询语句
            string query = "  {\"query\": {\"filtered\": {\"filter\": {\"and\": [" + queryAnd + "] }}},\"from\": " + from + ",\"size\": " + pageSize + ",\"sort\": {\"addtime\": { \"order\": \"asc\"}},\"highlight\": {\"fields\": { \"content\": {}} }}";


            var result = _elasticChatService.QueryBayConditions(query);
            return JsonResultHelper.CreateJson(result, true);
        }

        #endregion
    }
}
