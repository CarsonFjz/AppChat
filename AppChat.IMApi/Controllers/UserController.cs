using AppChat.Model;
using AppChat.Model.Request;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace AppChat.IMApi.Controllers
{
    public class UserController : ApiController
    {
        private IRequestClient<UserInitRequest, JsonResultModel> _userService;
        public UserController(IRequestClient<UserInitRequest, JsonResultModel> userService)
        {
            _userService = userService;
        }

        [Route("userinit"), HttpGet]
        public async Task<IHttpActionResult> UserLogin(UserInitRequest model)
        {
            var result = await _userService.Request(model);

            return Json(result);
        }
    }
}