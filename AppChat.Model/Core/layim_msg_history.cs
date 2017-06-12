using System;
using System.Linq;
using System.Text;

namespace AppChat.Model.Core
{
    public class layim_msg_history
    {
        
        /// <summary>
        /// Desc:- 
        /// Default:(newid()) 
        /// Nullable:False 
        /// </summary>
        public Guid id {get;set;}

        /// <summary>
        /// Desc:组id 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string gid {get;set;}

        /// <summary>
        /// Desc:消息内容 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string msg {get;set;}

        /// <summary>
        /// Desc:1 单聊 2 群聊 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Byte chattype {get;set;}

        /// <summary>
        /// Desc:添加时间 
        /// Default:((0)) 
        /// Nullable:False 
        /// </summary>
        public int addtime {get;set;}

        /// <summary>
        /// Desc:0 普通消息  1 系统消息 2 其他类型，待定 
        /// Default:((0)) 
        /// Nullable:False 
        /// </summary>
        public Byte msgtype {get;set;}

        /// <summary>
        /// Desc:聊天消息发送人，为0 就是系统消息 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Guid fromuser {get;set;}

    }
}
