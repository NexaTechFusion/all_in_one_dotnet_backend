using AIO.Application.Shared.DTOs.OperationResult;
using Mediator;
using AutoMapper;

namespace AIO.Application.Features.Product.Queries.GetList;

public class GetListProductQueryHandler(IMapper mapper): IRequestHandler<GetListProductQuery,
    OperationResult<List<GetListProductQueryResult>>>
{
    public async ValueTask<OperationResult<List<GetListProductQueryResult>>> Handle(GetListProductQuery request, CancellationToken cancellationToken)
    {
        var result = new List<Domain.Product.Entities.Product>
        {
            new Domain.Product.Entities.Product()
            {
                Id = 1,
                Name = "product",
                Quantity = 12,
                Type = "BOOK"
            }
        };
        var listProductQueryResult = new List<GetListProductQueryResult>(
            result.Select(mapper.Map<Domain.Product.Entities.Product, GetListProductQueryResult>).ToList()
        );
        return OperationResult<List<GetListProductQueryResult>>.SuccessResult(listProductQueryResult);
    }
}