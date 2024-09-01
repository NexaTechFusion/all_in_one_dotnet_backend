using AIO.Application.Shared.DTOs.OperationResult;
using AIO.Domain.Shared.Contracts.Persistence;
using AIO.Domain.Shared.Contracts.Persistence.Repository;
using Mediator;
using AutoMapper;

namespace AIO.Application.Features.Product.Queries.GetList;

public class GetListProductQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetListProductQuery,
    OperationResult<List<GetListProductQueryResult>>>
{
    public async ValueTask<OperationResult<List<GetListProductQueryResult>>> Handle(GetListProductQuery request,
        CancellationToken cancellationToken)
    {
        IRepository<Domain.Product.Entities.Product> productRepository =
            unitOfWork.GetRepository<Domain.Product.Entities.Product>(true);

        var result = productRepository.Query(null).Result
            .Select(mapper.Map<Domain.Product.Entities.Product, GetListProductQueryResult>).ToList();
        return OperationResult<List<GetListProductQueryResult>>.SuccessResult(result);
    }
}