using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppChat.Model.ViewModel
{
    /// <summary>
    /// 分页通用参数
    /// </summary>
    public class PageSearchOptions
    {
        public string TableName { get; set; }
        public string PrimaryKey { get; set; }
        public string Fields { get; set; }
        public string Condition { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public string Sort { get; set; }
    }
}
