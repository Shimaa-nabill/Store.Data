using Store.Service.HandleResponse;
using System.Net;
using System.Text.Json;

namespace Store.Web.MiddleWare
{
    public class ExpetionMiddleWare
    {
        private readonly RequestDelegate _next;
        private readonly IHostEnvironment _environment;
        private readonly ILogger<ExpetionMiddleWare> _logger;

        public ExpetionMiddleWare(RequestDelegate next,
            IHostEnvironment environment,
            ILogger<ExpetionMiddleWare>logger)
        {
           _next = next;
           _environment = environment;
           _logger = logger;
        }

        public async Task InvokeAsync( HttpContext context) 
        {
            try
            {
                await _next(context);
            }
            catch(Exception ex )
            {
                _logger.LogError(ex ,ex.Message);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode =(int)HttpStatusCode.InternalServerError;

                var response = _environment.IsDevelopment()
                    ? new CustomeException((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace)
                    : new CustomeException((int)HttpStatusCode.InternalServerError);

                var option =new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

                var json =JsonSerializer.Serialize(response, option);

                await context.Response.WriteAsync(json);
            }
           
        }
    }
}
