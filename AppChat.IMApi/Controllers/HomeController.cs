using AppChat.Model;
using AppChat.Model.Request;
using MassTransit;
using System.Threading.Tasks;
using System.Web.Http;

namespace AppChat.IMApi.Controllers
{
    [RoutePrefix("api/home")]
    public class HomeController : ApiController
    {
        private IRequestClient<LoginRequest, JsonResultModel> _loginService;
        private IRequestClient<AddUserRequest, JsonResultModel> _registService;
        public HomeController(IRequestClient<LoginRequest, JsonResultModel> loginService,
                              IRequestClient<AddUserRequest, JsonResultModel> registService)
        {
            _loginService = loginService;
            _registService = registService;
        }

        [AllowAnonymous]
        [Route("login"), HttpPost]
        public async Task<IHttpActionResult> UserLogin(LoginRequest model)
        {
            var result = await _loginService.Request(model);

            return Json(result);
        }

        [AllowAnonymous]
        [Route("regist"), HttpPost]
        public async Task<IHttpActionResult> RegistUser(AddUserRequest model)
        {
            var result = await _registService.Request(model);

            return Json(result);
        }
    }
}