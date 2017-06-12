using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppChat.Model.Message.DB
{
   public class LoginUser
    {
        public Guid userid { get; set; }
        public string LoginName { get; set; }
        public string PassWord { get; set; }
    }
}
