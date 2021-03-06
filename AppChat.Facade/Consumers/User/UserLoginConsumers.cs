﻿using AppChat.Cache;
using AppChat.Model.Request;
using AppChat.Service._Interface;
using MassTransit;
using System;
using System.Threading.Tasks;

namespace AppChat.Facade.Consumers
{
    public class UserLoginConsumers : IConsumer<LoginRequest>
    {
        private IUserLoginOrRegist _loginService;
        private IRedisCache _redisCacheService;

        public UserLoginConsumers(IUserLoginOrRegist loginService, IRedisCache redisCacheService)
        {
            _loginService = loginService;
            _redisCacheService = redisCacheService;
        }
        public async Task Consume(ConsumeContext<LoginRequest> context)
        {
            Guid userid = Guid.Empty;

            var result = _loginService.UserLogin(context.Message.username, context.Message.password, out userid);

            if (userid != Guid.Empty)
            {
                _redisCacheService.CacheUserAfterLogin(userid);
            }

            await context.RespondAsync(result);
        }
    }
}
