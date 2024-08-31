using AIO.Application.Shared.Profiles;

namespace AIO.Application.Features.Product.Commands.Add;

public class AddProductCommandResult : ICreateMapper<Domain.Product.Entities.Product>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
    public int Quantity { get; set; }

    public void Map(AutoMapper.Profile profile)
    {
        profile.CreateMap<Domain.Product.Entities.Product, AddProductCommandResult>()
            .ForMember(x => x.Id, dest => dest.MapFrom(x => x.Id))
            .ForMember(x => x.Name, dest => dest.MapFrom(x => x.Name))
            .ForMember(x => x.Type, dest => dest.MapFrom(x => x.Type))
            .ForMember(x => x.Quantity, dest => dest.MapFrom(x => x.Quantity));
    }
}