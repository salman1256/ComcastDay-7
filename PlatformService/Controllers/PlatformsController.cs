using System.Xml.Serialization;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Dtos;
using PlatformService.Models;
using PlatformService.Repos;
using PlatformService.SyncDataService.Http;

namespace PlatformService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformsController:ControllerBase
    {
        private readonly IPlatformRepo _repo;
        private readonly IMapper _mapper;
        private readonly ICommandDataClient _commandDataClient;
        public PlatformsController(IPlatformRepo repo,IMapper mapper,ICommandDataClient commandDataClient)
        {
            _repo=repo;
            _mapper=mapper;
            _commandDataClient=commandDataClient;
            
        }
        [HttpGet]
        public ActionResult<PlatformReadDto>GetPlatforms()
        {
            System.Console.WriteLine("Start Getting all Platforms");
            var allPlatforms=_repo.GetAllPlatforms();
            return Ok(_mapper.Map<IEnumerable<PlatformReadDto>>(allPlatforms));
        }
         [HttpGet("{id}",Name ="GetPlatformById")]
        public ActionResult<PlatformReadDto>GetPlatformById(int id)
        {
          var rcvdPlat=_repo.GetPlatform(id);
          if(rcvdPlat!=null)
          {
            return Ok(_mapper.Map<PlatformReadDto>(rcvdPlat));
          }
          else{return NotFound();}
        }
        [HttpPost]
          public async Task<ActionResult<PlatformReadDto>>CreatePlatform(PlatformCreateDto platformCreateDto)
        {

            var platformModel=_mapper.Map<Platform>(platformCreateDto);
            _repo.CreatePlatform(platformModel);
            _repo.SaveChanges();
            var platformReadDto=_mapper.Map<PlatformReadDto>(platformModel);
            try{
              await _commandDataClient.SendPlatformToCommand(platformReadDto);
            }
            catch(Exception ex)
            {
                System.Console.WriteLine("Cound not send message synchronosly error message !!!"+ex.Message);
            }
            return CreatedAtRoute(nameof(GetPlatformById),new {Id=platformReadDto.Id},platformReadDto);


        }


    }
    }
