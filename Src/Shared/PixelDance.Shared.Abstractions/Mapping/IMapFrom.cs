using AutoMapper;

namespace PixelDance.Shared.Abstractions.Mapping
{
    public interface IMapFrom<T>
    {
        void Mapping(Profile profile) => profile.CreateMap(typeof(T), GetType());
    }
}
