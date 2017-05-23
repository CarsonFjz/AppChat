using System;
using System.Linq;
using System.Text;

namespace AppChat.Model
{
    public class layim_user
    {
        
        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public int id {get;set;}

        /// <summary>
        /// Desc:用户登录名 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string loginname {get;set;}

        /// <summary>
        /// Desc:密码 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string loginpwd {get;set;}

        /// <summary>
        /// Desc:昵称 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string nickname {get;set;}

        /// <summary>
        /// Desc:签名 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string sign {get;set;}

        /// <summary>
        /// Desc:头像 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string headphoto {get;set;}

        /// <summary>
        /// Desc:创建时间 
        /// Default:(getdate()) 
        /// Nullable:False 
        /// </summary>
        public DateTime addtime {get;set;}

        /// <summary>
        /// Desc:IM号 
        /// Default:((0)) 
        /// Nullable:False 
        /// </summary>
        public int im {get;set;}

    }
}
