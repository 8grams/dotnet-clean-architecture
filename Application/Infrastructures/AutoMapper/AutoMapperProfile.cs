using System.Reflection;
using AutoMapper;
using WebApi.Application.Interfaces;
using WebApi.Application.Misc;

namespace WebApi.Application.Infrastructures.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public IWebApiDbContext _context { set; get; }
        public Utils _utils { set; get; }
        public AutoMapperProfile(IWebApiDbContext context, Utils utils)
        {
            _context = context;
            _utils = utils;
            
            LoadStandardMappings();
            LoadCustomMappings();
            LoadConverters();
        }

        private void LoadConverters()
        {

        }

        private void LoadStandardMappings()
        {
            var mapsFrom = MapperProfileHelper.LoadStandardMappings(Assembly.GetExecutingAssembly());
            foreach(var map in mapsFrom)
            {
                CreateMap(map.Source, map.Destination).ReverseMap();
            }
        }

        private void LoadCustomMappings()
        {
            var mapsFrom = MapperProfileHelper.LoadCustomMappings(Assembly.GetExecutingAssembly());
            foreach(var map in mapsFrom)
            {
                map.CreateMappings(this);
            }
        }

        public string GetFullUrl(string path)
        {
            return _utils.GetValidUrl(path);
        }
    }
}
