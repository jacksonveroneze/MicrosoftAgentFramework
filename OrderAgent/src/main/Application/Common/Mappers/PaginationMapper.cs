using JacksonVeroneze.OrderAgent.Application.Common.Models.Common.Response;
using JacksonVeroneze.NET.Pagination.Offset;
using Mapster;

namespace JacksonVeroneze.OrderAgent.Application.Common.Mappers;

public class PaginationMapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        ArgumentNullException.ThrowIfNull(config);

        config.NewConfig<PageInfo, PageInfoResponse>()
            .Map(dest => dest.Page, src => src.Page)
            .Map(dest => dest.PageSize, src => src.PageSize)
            .Map(dest => dest.TotalPages, src => src.TotalPages)
            .Map(dest => dest.TotalElements, src => src.TotalElements)
            .Map(dest => dest.IsFirstPage, src => src.IsFirstPage)
            .Map(dest => dest.IsLastPage, src => src.IsLastPage)
            .Map(dest => dest.HasNextPage, src => src.HasNextPage)
            .Map(dest => dest.HasBackPage, src => src.HasBackPage)
            .Map(dest => dest.NextPage, src => src.NextPage)
            .Map(dest => dest.BackPage, src => src.BackPage);
    }
}
