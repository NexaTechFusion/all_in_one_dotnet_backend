using AIO.Application.Shared.DTOs.OperationResult;
using AIO.Domain.Product.Aggregates;
using AIO.Domain.Shared.Contracts.Persistence;
using AutoMapper;
using Mediator;

namespace AIO.Application.Features.Product.Commands.Add;

public class AddProductCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<AddProductCommand,
    OperationResult<AddProductCommandResult>>
{
    public async ValueTask<OperationResult<AddProductCommandResult>> Handle(AddProductCommand request,
        CancellationToken cancellationToken)
    {
        Domain.Product.Entities.Product product = ProductManager.Add(request.Name, request.type, request.quantity);
        await unitOfWork.GetRepository<Domain.Product.Entities.Product>().Add(product);
        await unitOfWork.CommitAsync();
        AddProductCommandResult result = mapper.Map<Domain.Product.Entities.Product, AddProductCommandResult>(product);
        return OperationResult<AddProductCommandResult>.SuccessResult(result);
    }
}