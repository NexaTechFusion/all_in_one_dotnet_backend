using AutoMapper;

namespace AIO.Application.Shared.Profiles;

public interface IMapTo<TDestination>
{
    void Map(Profile profile)
    {
        profile.CreateMap(GetType(), typeof(TDestination)).ReverseMap();
    }
}