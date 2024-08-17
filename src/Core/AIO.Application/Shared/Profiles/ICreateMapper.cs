using AutoMapper;

namespace AIO.Application.Shared.Profiles;

public interface ICreateMapper<TSource>
{
    void Map(Profile profile)
    {
        profile.CreateMap(typeof(TSource), GetType()).PreserveReferences().ReverseMap();
    }
}