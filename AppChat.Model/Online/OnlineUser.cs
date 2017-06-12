using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppChat.Model.Online
{
    /// <summary>
    /// 在线用户
    /// </summary>
    public class OnlineUser
    {
        public Guid userId { get; set; }
        public string connectionid { get; set; }
    }
}
