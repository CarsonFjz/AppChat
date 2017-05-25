﻿using System;
using System.Linq;
using System.Text;

namespace Models
{
    public class layim_msg_unread
    {
        
        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Guid id {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Guid gid {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public int uid {get;set;}

        /// <summary>
        /// Desc:未读消息个数 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public int msgcount {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:(getdate()) 
        /// Nullable:False 
        /// </summary>
        public DateTime udatetime {get;set;}

    }
}
