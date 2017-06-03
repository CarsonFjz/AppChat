using AppChat.Cache;
using AppChat.IMApi.Models;
using AppChat.Service._Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;

namespace AppChat.IMApi.Controllers
{
    [RoutePrefix("api/home")]
    public class HomeController : ApiController
    {
        private IUserLoginOrRegist _loginService;
        private IRedisCache _redisCacheService;
        public HomeController(IUserLoginOrRegist loginService, IRedisCache redisCacheService)
        {
            _loginService = loginService;
            _redisCacheService = redisCacheService;
        }

        [Route("login"), HttpPost]
        public async Task<IHttpActionResult> UserLogin(LoginModel model)
        {
            int userid = 0;

            var result = _loginService.UserLoginOrRegister(model.username, model.password, out userid);

            if (userid > 0)
            {
                await _redisCacheService.CacheUserAfterLogin(userid);
            }

            return Json(result);
        }

        [Route("regist"), HttpPost]
        public async Task<IHttpActionResult> RegistUser(AddUserBindingModel model)
        {
            await _redisCacheService.CacheUserAfterLogin(1);

            return Json(new { id = 1 });
        }
    }
}