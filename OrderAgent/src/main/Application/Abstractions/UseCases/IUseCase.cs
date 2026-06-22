namespace JacksonVeroneze.OrderAgent.Application.Abstractions.UseCases;

public interface IBaseRequest;

public interface IResponse;

public interface IUseCase<in TRequest, TResponse>
    where TRequest : IBaseRequest
{
    Task<TResponse> ExecuteAsync(
        TRequest request,
        CancellationToken cancellationToken);
}
