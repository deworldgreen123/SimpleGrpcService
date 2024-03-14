using Grpc.Core;

namespace hw2.Interceptor;

public class LogInterceptor: Grpc.Core.Interceptors.Interceptor
{
    private readonly ILogger<LogInterceptor> _logger;

    public LogInterceptor(ILogger<LogInterceptor> logger)
    {
        _logger = logger;
    }

    public override async Task<TResponse> UnaryServerHandler<TRequest, TResponse>(TRequest request, ServerCallContext context,
        UnaryServerMethod<TRequest, TResponse> continuation)
    {
        _logger.LogInformation("Start of the method call {Method} with the request {request},", 
            context.Method, 
            request);
        var response = await continuation(request, context);
        _logger.LogInformation("End of {Method} with request = {request}, response = {response}", 
            context.Method, 
            request,response);
        return response;
    }

}