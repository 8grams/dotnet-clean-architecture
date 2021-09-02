using AutoMapper;

namespace WebApi.Application.Interfaces.Mappings
{
    public interface IHaveCustomMapping
    {
        void CreateMappings(Profile profile);
    }
}