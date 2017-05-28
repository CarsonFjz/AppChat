using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
    基础实体
*/
namespace AppChat.Model
{


    /// <summary>
    /// 基类
    /// </summary>
    public class AvatarIntEntity
    {
        public int id { get; set; }
        public string avatar { get; set; } 
    }
    public class AvatarGuidEntity
    {
        public Guid id { get; set; }
        public string avatar { get; set; }
    }

    /// <summary>
    /// 用户
    /// </summary>
    public class UserEntity : AvatarIntEntity
    {
        public string status { get; set; }
        public string username { get; set; }
        public string sign { get; set; }
    }

    /// <summary>
    /// 好友
    /// </summary>
    public class GroupUserEntity : UserEntity
    {
        public Guid groupid { get; set; }
        public string remarkname { get; set; }
    }

    /// <summary>
    ///  群
    /// </summary>
    public class GroupEntity : AvatarGuidEntity
    {
        public string groupname { get; set; }
        public string groupdesc { get; set; }
    }

    /// <summary>
    /// 好友分组
    /// </summary>
    public class FriendGroupEntity : GroupEntity
    {
        public FriendGroupEntity() {
            //好友分组，该选项不需要
            avatar = "";
        }
        public IEnumerable<GroupUserEntity> list { get; set; }
        public int online { get; set; }
    }
}

/// <summary>
/// 返回结果实体
/// </summary>
namespace AppChat.Model
{
    /// <summary>
    /// 基础信息json
    /// </summary>
    public class BaseListResult
    {
        public BaseListResult()
        {
        }
        public IEnumerable<FriendGroupEntity> friend { get; set; }
        public IEnumerable<GroupEntity> group { get; set; }
        public UserEntity mine { get; set; }
        /// <summary>
        /// 用户设置的皮肤
        /// </summary>
        public List<string> skin { get; set; }
    }

    /// <summary>
    /// 群员信息json
    /// </summary>
    public class MembersListResult
    {
        /// <summary>
        /// 群主
        /// </summary>
        public UserEntity owner { get; set; }
        /// <summary>
        /// 群成员列表
        /// </summary>
        public List<GroupUserEntity> list { get; set; }
    }

    /// <summary>
    /// 返回结果
    /// </summary>
    public class JsonResultModel
    {
        public JsonResultType code { get; set; }
        public object data { get; set; }
        public string msg { get; set; }
    }

    /// <summary>
    /// 成功失败
    /// </summary>
    public enum JsonResultType
    {
        Success = 0,
        Failed = 1
    }
}