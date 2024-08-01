using Microsoft.EntityFrameworkCore;
using PlatformService.Data;
using PlatformService.Repos;
using PlatformService.SyncDataService.Http;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options=>options.UseInMemoryDatabase("InMem"));
builder.Services.AddScoped<IPlatformRepo,PlatformRepo>();
builder.Services.AddHttpClient<ICommandDataClient,CommandDataClient>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();
System.Console.WriteLine("Command Service is Running on EndPoint "+builder.Configuration["CommandService"]);

app.UseAuthorization();

app.MapControllers();
PrepDb.PrepData(app);

app.Run();

