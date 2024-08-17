using AIO.Application.Shared.Profiles;

namespace AIO.Application.Features.Product.Queries.GetList;

public class GetListProductQueryResult : ICreateMapper<Domain.Product.Entities.Product>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
    public int Quantity { get; set; }
}