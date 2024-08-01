using AutoMapper;
using CommandService.Dto;
using CommandService.Repos;
using Microsoft.AspNetCore.Mvc;

namespace CommandService.Controllers
{   [Route("api/c/[controller]")]
    [ApiController]
    public class PlatformsController:ControllerBase
    {
        private readonly ICommandRepo _repo;
        private readonly IMapper _mapper;

        public PlatformsController(ICommandRepo repo,IMapper mapper)
        {
            _repo=repo;
            _mapper=mapper;
        }
        [HttpGet]
        public ActionResult<IEnumerable<PlatformReadDto>>GetPlatforms()
        {
            System.Console.WriteLine("****Getting Started Platforms from Command Service");
            var rcvdPlatforms=_repo.GetAllPlatforms();
            return Ok(_mapper.Map<IEnumerable<PlatformReadDto>>(rcvdPlatforms));
        }
        [HttpPost]
        public ActionResult TestInBoundConnection()
        {
            System.Console.WriteLine("In Bound Post Command Service Started!!!");
            return Ok("Inbound Test of Command Service from Platform Controller Success");
        }
    }
}