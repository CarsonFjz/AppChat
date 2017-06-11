using AppChat.Model;
using AppChat.Model.Request;
using AppChat.Service._Interface;
using AppChat.Utils.JsonResult;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppChat.Facade.Consumers.User
{
    public class UserInitConsumers : IConsumer<UserInitRequest>
    {
        private IUserService _userService;
        public UserInitConsumers(IUserService userService)
        {
            _userService = userService;
        }

        public Task Consume(ConsumeContext<UserInitRequest> context)
        {
            var result = _userService.GetChatRoomBaseInfo(context.Message.userid);

            return JsonResultHelper.CreateJsonAsync(result);
        }
    }
}
