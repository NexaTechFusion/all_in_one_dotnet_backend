namespace AIO.Domain.Product.Aggregates;

public static class ProductManager
{
    public static Entities.Product Add(string name, string type, int quantity)
    {
        return new Entities.Product
        {
            Name = name,
            Type= type,
            Quantity = quantity
        };
    }
    
}