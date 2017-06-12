using System;
using System.Linq;
using System.Text;

namespace AppChat.Model.Core
{
    public class layim_sys_user_log
    {
        
        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public int id {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string ip {get;set;}

        /// <summary>
        /// Desc:1 登录系统 2 登出系统 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Byte operate {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:(getdate()) 
        /// Nullable:False 
        /// </summary>
        public DateTime addtime {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Guid uid {get;set;}

    }
}
