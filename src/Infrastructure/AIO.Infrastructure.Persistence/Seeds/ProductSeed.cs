using AIO.Domain.Product.Aggregates;
using AIO.Domain.Product.Entities;
using AIO.Domain.Shared.Contracts.Persistence;
using AIO.Domain.Shared.Contracts.Persistence.Repository;

namespace AIO.Infrastructure.Persistence.Seeds;

public class ProductSeed(IUnitOfWork unitOfWork)
{
    public async Task Run()
    {
        IRepository<Product> productRepository = unitOfWork.GetRepository<Product>();
        var products = new Product[]
        {
            new ()
            {
                Name = "Sample Product 1",
                Quantity = 12,
                Type = "Book"
            },
            new ()
            {
                Name = "Sample Product 2",
                Quantity = 10,
                Type = "Pen"
            }
        };
        await productRepository.MultiAdd(products);
        await unitOfWork.CommitAsync();


    }
}