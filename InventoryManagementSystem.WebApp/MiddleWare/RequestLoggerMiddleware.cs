namespace InventoryManagementSystem.WebApp.MiddleWare
{
    public class RequestLoggerMiddleware
    {
        private readonly RequestDelegate requestDelegate;

        public RequestLoggerMiddleware(RequestDelegate requestDelegate)
        {
            this.requestDelegate = requestDelegate;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            Console.WriteLine($"Incoming Request: {context.Request.Method} {context.Request.Path} at {DateTime.UtcNow}");
            await requestDelegate(context);
            Console.WriteLine($"Outgoing Response: {context.Response.StatusCode} at {DateTime.UtcNow}");
        }
    }
}
