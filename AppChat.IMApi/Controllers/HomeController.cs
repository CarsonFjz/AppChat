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
        public HomeController(IRequestClient<LoginRequest, JsonResultModel> loginService)
        {
            _loginService = loginService;
        }

        [AllowAnonymous]
        [Route("login"), HttpPost]
        public async Task<IHttpActionResult> UserLogin(LoginRequest model)
        {
            var result = await _loginService.Request(model);

            return Json(result);
        }

        //[Route("regist"), HttpPost]
        //public async Task<IHttpActionResult> RegistUser(AddUserRequest model)
        //{
        //    await _redisCacheService.CacheUserAfterLogin(1);

        //    return Json(new { id = 1 });
        //}
    }
}