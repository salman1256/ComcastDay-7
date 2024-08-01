using System.ComponentModel.Design;
using AutoMapper;
using CommandService.Dto;
using CommandService.Models;
using CommandService.Repos;
using Microsoft.AspNetCore.Mvc;

namespace CommandService.Controllers
{
   [Route("api/c/platforms/{platformId}[controller]")]
    [ApiController]
    public class CommandsController:ControllerBase
    {
        private readonly ICommandRepo _repo;
        private readonly IMapper _mapper;
        public CommandsController(ICommandRepo repo,IMapper mapper)
        {
            _repo=repo;
            _mapper=mapper;
        }
        [HttpGet]
        public ActionResult<IEnumerable<CommandReadDto>>GetCommandsForPlatform(int platformId)
        {
            System.Console.WriteLine($"**** Getting Started Commands for PlatformId {platformId} *****");
            if(!_repo.PlatformExists(platformId))
            {
                return NotFound();
            }
            var Commands =_repo.GetAllCommandsForPlatform(platformId);
            return Ok(_mapper.Map<IEnumerable<CommandReadDto>>(Commands));
        }

         [HttpGet("{commandId}",Name ="GetCommandForPlatform")]
        public ActionResult<CommandReadDto>GetCommandForPlatform(int platformId,int commandId)
        {
           System.Console.WriteLine($"Specified Command for PlatformId {platformId} and CommandId {commandId}");
           if(!_repo.PlatformExists(platformId))
           {
            return NotFound();
           }
           var command=_repo.GetCommandForPlatform(platformId,commandId);
                       return Ok(_mapper.Map<CommandReadDto>(command));
        }
        [HttpPost]
        public ActionResult<CommandCreatedDto>CreateCommandForPlatform(int platformId,CommandCreatedDto commandCreatedDto)
        {
            System.Console.WriteLine($"Creating Command for PlatformId {platformId}");
            if(!_repo.PlatformExists(platformId))
            {
                return NotFound();
            }
            var command=_mapper.Map<Command>(commandCreatedDto);
            _repo.CreateCommandForPlatform(platformId,command);
            _repo.SaveChanges();
            var commandReadDto=_mapper.Map<CommandReadDto>(command);

            return CreatedAtRoute(nameof(GetCommandForPlatform),new {platformId=platformId,CommandID=commandReadDto.Id},commandReadDto);
        }

    }
    }
    