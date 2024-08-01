using AutoMapper;
using CommandService.Dto;
using CommandService.Models;

namespace CommandService.Profiles
{
    public class CommandProfile:Profile
    {
       
        public CommandProfile()
        {
            CreateMap<Platform,PlatformReadDto>();
            CreateMap<CommandCreatedDto,Command>();
            CreateMap<Command,CommandReadDto>();
        }
    }
}