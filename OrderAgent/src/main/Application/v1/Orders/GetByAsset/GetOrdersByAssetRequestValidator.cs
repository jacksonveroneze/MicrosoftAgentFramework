using FluentValidation;

namespace JacksonVeroneze.OrderAgent.Application.v1.Orders.GetByAsset;

public sealed class GetOrdersByAssetRequestValidator
    : AbstractValidator<GetOrdersByAssetRequest>
{
    public GetOrdersByAssetRequestValidator()
    {
        RuleFor(request => request.UserId)
            .NotEmpty();

        RuleFor(request => request.AccountId)
            .NotEmpty();

        RuleFor(request => request.Ticker)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .MinimumLength(4)
            .MaximumLength(10)
            .Matches("^[A-Z0-9]+$");
    }
}
