namespace MiddlewarePraxtices.Middlewares
{
    public class HelloMiddleware
    {
        private readonly RequestDelegate _next;
        public HelloMiddleware(RequestDelegate next)
        {
            _next = next;

        }

        public async Task Inkove(HttpContext context)
        {
            Console.WriteLine("hello world!");
            await _next.Invoke(context);
            Console.WriteLine("bye world");
        }
    }
    static public class HelloMiddlewareEntension
    {
        public static IApplicationBuilder UseHello(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<HelloMiddleware>();
        }
    }
}