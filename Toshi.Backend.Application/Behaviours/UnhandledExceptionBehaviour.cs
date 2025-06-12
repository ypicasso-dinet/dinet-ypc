using MediatR;

namespace Toshi.Backend.Application.Behaviours
{
    public class UnhandledExceptionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        public UnhandledExceptionBehaviour()
        {
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            try
            {
                return await next();
            }
            catch (Exception ex)
            {
                var requestName = typeof(TRequest).Name;
                var message = $"Application Request: Sucedio una excepción para el request {requestName} {request}";

                throw;
            }
        }
    }
}
