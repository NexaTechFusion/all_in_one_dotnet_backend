using AIO.Application.Shared.DTOs.OperationResult;
using Mediator;

namespace AIO.Application.Features.Product.Queries.GetList;

public record GetListProductQuery : IRequest<OperationResult<List<GetListProductQueryResult>>>
{
    
}