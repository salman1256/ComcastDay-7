
using CommandService.Models;

namespace CommandService.Repos
{
public interface ICommandRepo
{
    bool SaveChanges();
    IEnumerable<Platform>GetAllPlatforms();
    void CreatePlatform(Platform platform);
    bool PlatformExists(int platformId);
    bool ExternalPlatformExists(int externalPlatforomId);
    IEnumerable<Command> GetAllCommandsForPlatform(int platformId);
    Command GetCommandForPlatform(int platformId,int commandId);
    void CreateCommandForPlatform(int platformId,Command command);
}
}
