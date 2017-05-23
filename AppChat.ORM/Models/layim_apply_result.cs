using System;
using System.Linq;
using System.Text;

namespace Models
{
    public class layim_apply_result
    {
        
        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public int id {get;set;}

        /// <summary>
        /// Desc: 申请表主键id 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public int applyid {get;set;}

        /// <summary>
        /// Desc:操作人id 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public int operateid {get;set;}

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

    }
}
