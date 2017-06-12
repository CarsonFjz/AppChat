using System;
using System.Linq;
using System.Text;

namespace AppChat.Model.Core
{
    public class layim_group
    {
        
        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Guid id {get;set;}

        /// <summary>
        /// Desc:群组名称 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string name {get;set;}

        /// <summary>
        /// Desc:群头像 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string headphoto {get;set;}

        /// <summary>
        /// Desc:群描述 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string groupdesc {get;set;}

        /// <summary>
        /// Desc:创建时间 
        /// Default:(getdate()) 
        /// Nullable:False 
        /// </summary>
        public DateTime addtime {get;set;}

        /// <summary>
        /// Desc:群主 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public int groupowner {get;set;}

        /// <summary>
        /// Desc:群状态，0正常 1已经解散 
        /// Default:((0)) 
        /// Nullable:False 
        /// </summary>
        public int status {get;set;}

        /// <summary>
        /// Desc:是否系统默认群 
        /// Default:((0)) 
        /// Nullable:False 
        /// </summary>
        public Boolean issystem {get;set;}

        /// <summary>
        /// Desc:最大人数限制 
        /// Default:((200)) 
        /// Nullable:False 
        /// </summary>
        public int pcount {get;set;}

        /// <summary>
        /// Desc:IM号 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Guid uid {get;set;}

    }
}
