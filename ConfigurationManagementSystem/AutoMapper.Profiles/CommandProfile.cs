using AutoMapper;
using ConfigurationManagementSystem.Models.InputModels;
using ConfigurationManagementSystem.Models.OutputModels;
using Databases.Entities;

namespace ConfigurationManagementSystem.AutoMapper.Profiles
{
    public class CommandProfile : Profile
    {
        public CommandProfile() 
        {
            CreateMap<CommandInputModel, Command>();
            CreateMap<Command, CommandOutputModel>();
        }
    }
}
