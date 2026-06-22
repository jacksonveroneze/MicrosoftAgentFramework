using JacksonVeroneze.NET.Pagination.Enums;

namespace JacksonVeroneze.OrderAgent.Application.Common.Models.Common.Request;

public abstract record PagedRequest
{
    private const int DefaultPage = 1;

    private const int DefaulPageSize = 20;

    protected PagedRequest(
        string defaultOrderBy,
        SortDirection defaultOrder)
    {
        ArgumentException.ThrowIfNullOrEmpty(defaultOrderBy);

        _orderBy = defaultOrderBy;
        _order = defaultOrder;

        _page = DefaultPage;
        _pageSize = DefaulPageSize;
    }

    private readonly int? _page;
    private readonly int? _pageSize;
    private readonly string? _orderBy;
    private readonly SortDirection? _order;

    public int? Page
    {
        get => _page;
        init => _page = value != 0 ? value : DefaultPage;
    }

    public int? PageSize
    {
        get => _pageSize;
        init => _pageSize = value != 0 ? value : DefaulPageSize;
    }

    public string? OrderBy
    {
        get => _orderBy;
        init => _orderBy = value ?? _orderBy;
    }

    public SortDirection? Order
    {
        get => _order;
        init => _order = value ?? _order;
    }
}
