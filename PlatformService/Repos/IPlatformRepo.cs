using System.ComponentModel.DataAnnotations;
using PlatformService.Models;

namespace PlatformService.Repos
{
    public interface IPlatformRepo
    {
        bool SaveChanges();
        IEnumerable<Platform> GetAllPlatforms();
        Platform GetPlatform(int id);
        void CreatePlatform(Platform platform);

    }

}