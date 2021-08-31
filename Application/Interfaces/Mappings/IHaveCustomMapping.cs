using AutoMapper;

namespace SFIDWebAPI.Application.Interfaces.Mappings
{
    public interface IHaveCustomMapping
    {
        void CreateMappings(Profile profile);
    }
}