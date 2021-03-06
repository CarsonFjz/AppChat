﻿using System;
using System.Linq;
using System.Text;

namespace AppChat.Model.Core
{
    public class layim_apply_result
    {
        
        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Guid id {get;set;}

        /// <summary>
        /// Desc: 申请表主键id 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Guid applyid {get;set;}

        /// <summary>
        /// Desc:操作时间 
        /// Default:(getdate()) 
        /// Nullable:False 
        /// </summary>
        public DateTime operatetime {get;set;}

        /// <summary>
        /// Desc:是否已读 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Boolean isread {get;set;}

        /// <summary>
        /// Desc:操作结果理由 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string reason {get;set;}

        /// <summary>
        /// Desc:操作人id 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Guid operateid {get;set;}

    }
}
