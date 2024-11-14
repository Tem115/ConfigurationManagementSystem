
using AutoMapper;
using ConfigurationManagementSystem.Models.OutputModels;
using Databases.Entities;
using Version = Databases.Entities.Version;

namespace ConfigurationManagementSystem.AutoMapper.Profiles
{
    public class VersionProfile : Profile
    {
        public VersionProfile() 
        {
            CreateMap<Version, VersionOutputModel>();
        }
    }
}
