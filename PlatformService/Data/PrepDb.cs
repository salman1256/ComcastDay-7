
using System;
using Microsoft.EntityFrameworkCore;
using PlatformService.Models;
namespace PlatformService.Data
{
   
    public static class PrepDb
    {
        public static void PrepData(IApplicationBuilder app, bool isProduction)
       {
        using (var serviceScope=app.ApplicationServices.CreateScope())
        {
            SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>(),isProduction);
        }
       }

        private static void SeedData(AppDbContext appDbContext,bool isProduction)
        {
            if(isProduction)
            {
                System.Console.WriteLine("Attempting to Apply Migrations..........");
                try{
                    appDbContext.Database.Migrate();
                }
                catch(Exception ex)
                {
                    System.Console.WriteLine("Could not run the migration due to error!!!"+ex.Message);
                }
            }

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