using FluentValidation;

namespace JacksonVeroneze.OrderAgent.Application.v1.Orders.GetOpen;

public sealed class GetOrdersOpenRequestValidator
    : AbstractValidator<GetOrdersOpenRequest>
{
    public GetOrdersOpenRequestValidator()
    {
        RuleFor(request => request.UserId)
            .NotEmpty();

        RuleFor(request => request.AccountId)
            .NotEmpty();
    }
}
