using System;
using System.Linq;
using System.Text;

namespace AppChat.Model.Core
{
    public class layim_friend_group_detail
    {
        
        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Guid id {get;set;}

        /// <summary>
        /// Desc:组id 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Guid gid {get;set;}

        /// <summary>
        /// Desc:添加时间 
        /// Default:(getdate()) 
        /// Nullable:False 
        /// </summary>
        public DateTime addtime {get;set;}

        /// <summary>
        /// Desc:用户自定义的备注 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string gname {get;set;}

        /// <summary>
        /// Desc:用户id 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Guid uid {get;set;}

    }
}
