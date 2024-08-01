
using System;
using PlatformService.Models;
namespace PlatformService.Data
{
   
    public static class PrepDb
    {
        public static void PrepData(IApplicationBuilder app)
       {
        using (var serviceScope=app.ApplicationServices.CreateScope())
        {
            SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>());
        }
       }

        private static void SeedData(AppDbContext? appDbContext)
        {
            if(!appDbContext.Platforms.Any())
            {
                Console.WriteLine("***** Seeding Data Started **** ");
                appDbContext.Platforms.AddRange
                ( 
                    new Platform() { Name = "Sql Server", Publisher = "Microsoft", Cost = "open Source" },
                    new Platform(){Name="Core dot net",Publisher="Microsoft", Cost="open Source"},
                    new Platform(){Name="Azure",Publisher="Microsoft", Cost="Cost Involved"},
                    new Platform(){Name="Kubernetes",Publisher="Cloud Native", Cost="open Source"}
                );
                appDbContext.SaveChanges();

            }
            else{
                Console.WriteLine("We have data there... No seeding is required");
            }
        }
    }

}