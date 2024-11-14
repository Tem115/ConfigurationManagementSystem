
using AutoMapper;
using ConfigurationManagementSystem.Models.InputModels;
using ConfigurationManagementSystem.Models.OutputModels;
using Databases.Entities;

namespace ConfigurationManagementSystem.AutoMapper.Profiles
{
    public class HotKeyProfile : Profile
    {
        public HotKeyProfile() 
        {
            CreateMap<HotKeyInputModel, HotKey>();
            CreateMap<HotKey, HotKeyOutputModel>();
        }
    }
}
