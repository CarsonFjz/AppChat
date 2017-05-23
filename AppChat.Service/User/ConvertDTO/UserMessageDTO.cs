using AppChat.Model.Core;
using AppChat.Model.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppChat.Service.User.ConvertDTO
{
    public class UserMessageDTO
    {
        public static ApplyMessage ConvertToApplyMessage(this layim_apply enitiy)
        {
            if (enitiy == null)
            {
                return new ArgumentException("layim_apply entity is null")
            }
        }
    }
}
