namespace JacksonVeroneze.OrderAgent.Application.Common.Models.Common.Response;

public record PageInfoResponse
{
    public int Page { get; init; }

    public int PageSize { get; init; }

    public int TotalPages { get; init; }

    public int TotalElements { get; init; }

    public bool? IsFirstPage { get; init; }

    public bool? IsLastPage { get; init; }

    public bool? HasNextPage { get; init; }

    public bool? HasBackPage { get; init; }

    public int? NextPage { get; init; }

    public int? BackPage { get; init; }
}
