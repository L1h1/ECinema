using MediatR;
using System.Security.Claims;
using System.Text;

public class UserActionLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public UserActionLoggingMiddleware(RequestDelegate next, IServiceScopeFactory serviceScopeFactory)
    {
        _next = next;
        _serviceScopeFactory = serviceScopeFactory;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        //Информация о пользователе и запросе
        var userId = httpContext.User.Identity.IsAuthenticated
            ? int.Parse(httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value)
            : 0;


        var actionType = httpContext.Request.Method; // Тип запроса (GET, POST, PUT, DELETE)
        var actionDetails = await GetRequestDetails(httpContext);

        //Область для разрешения Scoped зависимостей
        using (var scope = _serviceScopeFactory.CreateScope())
        {
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

            var logRequest = new InsertUserActionLogRequest
            {
                UserId = userId,
                ActionType = actionType,
                ActionDetails = actionDetails,
                ActionTime = DateTime.UtcNow
            };


            await mediator.Send(logRequest);
        }

        await _next(httpContext);


    }
    // Получение всех деталей запроса
    private async Task<string> GetRequestDetails(HttpContext httpContext)
    {
        var request = httpContext.Request;

        request.EnableBuffering(); //Многократное считывание тела запроса
        var buffer = new byte[Convert.ToInt32(request.ContentLength)];
        await request.Body.ReadAsync(buffer, 0, buffer.Length);
        var body = Encoding.UTF8.GetString(buffer);

        // Сброс потока обратно, чтобы другие middleware и контроллеры могли его обработать
        request.Body.Seek(0, SeekOrigin.Begin);


        var details = new StringBuilder();
        details.AppendLine($"Method: {request.Method}");
        details.AppendLine($"\nURL: {request.Path}{request.QueryString}");
        //details.AppendLine($"Headers: {string.Join(", ", request.Headers.Select(h => $"{h.Key}: {h.Value}"))}");
        if(!body.Contains("password"))
            details.AppendLine($"\nBody: {body}");

        return details.ToString();
    }
}
