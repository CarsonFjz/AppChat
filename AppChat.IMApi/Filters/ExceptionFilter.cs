using Autofac.Integration.WebApi;
using Serilog;
using System.Web.Http.Filters;
using System.Threading;
using System.Threading.Tasks;

namespace AppChat.IMApi.Filters
{
    public class ExceptionFilter : IAutofacExceptionFilter
    {
        private readonly ILogger _logger;

        public ExceptionFilter(ILogger logger)
        {
            this._logger = logger;
        }

        //错误统一出口
        public async Task OnExceptionAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
        {
            await Task.Run(() =>
            {
                _logger.Error(actionExecutedContext.Exception, actionExecutedContext.Exception.Message);
            });

        }

    }
}