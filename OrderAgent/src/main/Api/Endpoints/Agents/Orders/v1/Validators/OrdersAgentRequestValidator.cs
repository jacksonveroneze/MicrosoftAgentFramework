using FluentValidation;
using JacksonVeroneze.OrderAgent.Api.Endpoints.Agents.Orders.v1.Models;

namespace JacksonVeroneze.OrderAgent.Api.Endpoints.Agents.Orders.v1.Validators;

public sealed class OrdersAgentRequestValidator
    : AbstractValidator<OrdersAgentRequest>
{
    private const int PromptMinLength = 10;
    private const int PromptMaxLength = 500;

    public OrdersAgentRequestValidator()
    {
        RuleFor(request => request.Prompt)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .NotEmpty()
            .Length(PromptMinLength, PromptMaxLength);
    }
}
