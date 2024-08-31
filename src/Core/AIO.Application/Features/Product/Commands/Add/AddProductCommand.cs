using AIO.Application.Shared.DTOs.OperationResult;
using Mediator;

namespace AIO.Application.Features.Product.Commands.Add;

public record AddProductCommand : IRequest<OperationResult<AddProductCommandResult>>
{
    public string Name { get; init; }
    public int quantity { get; init; }
    public string type { get; init; }
}