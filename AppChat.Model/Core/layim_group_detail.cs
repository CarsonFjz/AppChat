using System;
using System.Linq;
using System.Text;

namespace AppChat.Model.Core
{
    public class layim_group_detail
    {
        
        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Guid id {get;set;}

        /// <summary>
        /// Desc:群id 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Guid gid {get;set;}

        /// <summary>
        /// Desc:0群成员 1 群主 2 群管理员 3游客（游客不能发言） 
        /// Default:((0)) 
        /// Nullable:False 
        /// </summary>
        public Byte urole {get;set;}

        /// <summary>
        /// Desc:0正常  1 禁言 2 剔除 
        /// Default:((0)) 
        /// Nullable:False 
        /// </summary>
        public Byte upower {get;set;}

        /// <summary>
        /// Desc:群昵称 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string remarkname {get;set;}

        /// <summary>
        /// Desc:加入时间 
        /// Default:(getdate()) 
        /// Nullable:False 
        /// </summary>
        public DateTime addtime {get;set;}

        /// <summary>
        /// Desc:用户id 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Guid uid {get;set;}

    }
}
