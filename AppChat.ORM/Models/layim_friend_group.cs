using System;
using System.Linq;
using System.Text;

namespace Models
{
    public class layim_friend_group
    {
        
        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public int id {get;set;}

        /// <summary>
        /// Desc:好友组名称 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string name {get;set;}

        /// <summary>
        /// Desc:分组归属 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public int belonguid {get;set;}

        /// <summary>
        /// Desc:组创建时间 
        /// Default:(getdate()) 
        /// Nullable:False 
        /// </summary>
        public DateTime addtime {get;set;}

        /// <summary>
        /// Desc:排序 
        /// Default:((0)) 
        /// Nullable:False 
        /// </summary>
        public Byte sort {get;set;}

        /// <summary>
        /// Desc:是否系统默认 
        /// Default:((0)) 
        /// Nullable:False 
        /// </summary>
        public Boolean issystem {get;set;}

    }
}
