﻿using AppChat.Cache;
using AppChat.Model;
using AppChat.Model.Convert;
using AppChat.Model.Core;
using AppChat.Service._Interface;
using AppChat.Utils;
using AppChat.Utils.Consts;
using AppChat.Utils.Cookie;
using AppChat.Utils.IDCreate;
using AppChat.Utils.JsonResult;
using AppChat.Utils.Random;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppChat.Service.User
{
    public class UserLoginOrRegist : IUserLoginOrRegist
    {
        private IRedisCache _redisCacheService;
        private SqlSugarClient _context;
        public UserLoginOrRegist(IRedisCache redisCache, SqlSugarClient context)
        {
            _redisCacheService = redisCache;
            _context = context;
        }


        /// <summary>
        /// 用户登陆或者注册，返回用户id如果为 0 说明密码错误
        /// </summary>
        /// <param name="loginName"></param>
        /// <param name="loginPwd"></param>
        /// <param name="nickName"></param>
        /// <returns></returns>
        public JsonResultModel UserLogin(string loginName, string loginPwd, out Guid userid)
        {
            userid = Guid.Empty;

            //TODO:判断用户是否存在,存在就获取信息 done
            var loginUser = _context.Queryable<layim_user>().Where(x => x.loginname == loginName && x.loginpwd == loginPwd).Single();

            if (loginUser != null)
            {
                return JsonResultHelper.CreateJson(loginUser);
            }
            return JsonResultHelper.CreateJson(false,"账号或者密码错误");

        }

        /// <summary>
        /// 注册新用户
        /// </summary>
        /// <param name="loginNmae"></param>
        /// <param name="loginPwd"></param>
        /// <param name="nickName"></param>
        /// <param name="sex"></param>
        /// <returns></returns>
        public JsonResultModel UserRegist(string loginNmae, string loginPwd, string nickName, bool sex)
        {
            //1.先验证是否存在该用户登录名
            if (true == _context.Queryable<layim_user>().Where(x => x.loginname == loginNmae).Any())
            {
                return JsonResultHelper.CreateJson(false,"该登陆名已经被注册");
            }

            //2.添加用户
            var userModel = new layim_user()
            {
                id = Generator.CreateRpcrt4Guid(),
                loginname = loginNmae,
                nickname = nickName,
                loginpwd = loginPwd,
                sex = sex,
                addtime = DateTimeConverter.DateTimeToInt(DateTime.Now)
            };

            var Identity = _context.Insertable(userModel).ExecuteCommand();

            if (Identity > 0)
            {
                return JsonResultHelper.CreateJson(true,"注册成功");
            }

            return JsonResultHelper.CreateJson(false,"注册失败,请联系管理员");
        }


    }
}
