using AppChat.Cache;
using AppChat.Utils.Consts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace AppChat.IMApi.Filters
{
    public class UserAuthorizeAttribute : IAuthorizationFilter
    {
        public bool AllowMultiple
        {
            get
            {
                return true;
            }
        }

        public async Task<HttpResponseMessage> ExecuteAuthorizationFilterAsync(HttpActionContext actionContext, CancellationToken cancellationToken, Func<Task<HttpResponseMessage>> continuation)
        {
            if (SkipAuthentication(actionContext))
            {
                return await continuation();
            }

            var requestScope = actionContext.Request.GetDependencyScope();

            var service = requestScope.GetService(typeof(IRedisCache)) as IRedisCache;

            var userid = service.GetCurrentUserId();

            if (userid == TipsConst.cookieIsEmpty)
            {
                return new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.Unauthorized,
                    RequestMessage = actionContext.Request
                };
            }
            
            return await continuation();
        }


        //跳过不用验证的控制器[AllowAnonymousAttribute]
        private bool SkipAuthentication(HttpActionContext actionContext)
        {
            if (actionContext == null)
            {
                throw new ArgumentNullException("actionContext");
            }

            return actionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any()
                || actionContext.ControllerContext.ControllerDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any();
        }
    }
}