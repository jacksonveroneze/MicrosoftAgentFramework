namespace JacksonVeroneze.OrderAgent.Application.Common.Models.Common.Response;

public record PagedResponse<TType> 
    : DataResponse<TType>
{
    public PageInfoResponse? Pagination { get; init; }
}
