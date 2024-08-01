using System.Text;
using System.Text.Json;
using PlatformService.Dtos;

namespace PlatformService.SyncDataService.Http
{
    public class CommandDataClient : ICommandDataClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        public CommandDataClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient=httpClient;
            _configuration=configuration;
        }
        public async Task SendPlatformToCommand(PlatformReadDto platform)
        {
            var httpContent=new StringContent(JsonSerializer.Serialize(platform),Encoding.UTF8,"application/json");
            var response =await _httpClient.PostAsync($"{_configuration["CommandService"]}",httpContent);
            if(response.IsSuccessStatusCode)
            {
                System.Console.WriteLine("Synchornous Data Post Service working perfectly fine");
            }
            else{
                System.Console.WriteLine("Sync Data Post service not reached or error occured");
            }
        }
    }
}