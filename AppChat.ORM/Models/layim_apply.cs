using System;
using System.Linq;
using System.Text;

namespace Models
{
    public class layim_apply
    {
        
        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Guid id {get;set;}

        /// <summary>
        /// Desc:用户ID 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public int userid {get;set;}

        /// <summary>
        /// Desc:申请类型 0 好友 1群 
        /// Default:((0)) 
        /// Nullable:False 
        /// </summary>
        public Byte applytype {get;set;}

        /// <summary>
        /// Desc:被申请人id或者群id 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public int targetid {get;set;}

        /// <summary>
        /// Desc:申请时间 
        /// Default:(getdate()) 
        /// Nullable:False 
        /// </summary>
        public DateTime applytime {get;set;}

        /// <summary>
        /// Desc:申请备注 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string other {get;set;}

        /// <summary>
        /// Desc:审批结果：0未审批 1 同意 2 拒绝 
        /// Default:((0)) 
        /// Nullable:False 
        /// </summary>
        public Byte result {get;set;}

    }
}
