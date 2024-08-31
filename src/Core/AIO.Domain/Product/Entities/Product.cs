using AIO.Domain.Shared.Entities;

namespace AIO.Domain.Product.Entities;

public class Product: BaseEntity
{
    public string Name { get; set; }
    public string Type { get; set; }
    public int Quantity { get; set; }
}