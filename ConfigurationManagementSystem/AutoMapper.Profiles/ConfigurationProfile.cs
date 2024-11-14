using AutoMapper;
using ConfigurationManagementSystem.Models.InputModels;
using ConfigurationManagementSystem.Models.OutputModels;
using Databases.Entities;

namespace ConfigurationManagementSystem.AutoMapper.Profiles
{
    public class ConfigurationProfile : Profile
    {
        public ConfigurationProfile() 
        {
            CreateMap<ConfigurationInputModel, Configuration>();
            CreateMap<Configuration, ConfigurationOutputModel>();
        }
    }
}
