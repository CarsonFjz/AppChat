using AppChat.Cache;
using AppChat.Model.Core;
using AppChat.Model.Request;
using AppChat.Service._Interface;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppChat.Facade.Consumers
{
    public class UserRegistConsumers : IConsumer<AddUserRequest>
    {
        private IUserLoginOrRegist _registService;

        public UserRegistConsumers(IUserLoginOrRegist registService)
        {
            _registService = registService;
        }

        public async Task Consume(ConsumeContext<AddUserRequest> context)
        {
            var result = await _registService.UserRegist(context.Message.username, context.Message.password, context.Message.nickname, context.Message.sex);

            await context.RespondAsync(result);
        }
    }
}
