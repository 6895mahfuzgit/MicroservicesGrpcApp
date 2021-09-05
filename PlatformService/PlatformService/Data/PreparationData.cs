using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using PlatformService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlatformService.Data
{
    public static class PreparationData
    {
        public static void PolulateData(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<ApplicationDBContext>());
            }
        }

        private static void SeedData(ApplicationDBContext applicationDBContext)
        {
            if (!applicationDBContext.Platforms.Any())
            {
                Console.WriteLine("Data Seeding Started");
                applicationDBContext.AddRange(
                    new Platform() { Name = "Dot Net", Publisher = "Microsoft", Cost = "Free" },
                    new Platform() { Name = "SQL Server", Publisher = "Microsoft", Cost = "Free" },
                    new Platform() { Name = "Kubernetes", Publisher = "CNCF", Cost = "Free" }
                    );

                applicationDBContext.SaveChanges();
                Console.WriteLine("Data Seeded Successfully.");
            }
            else
            {
                Console.WriteLine("Contains Data");
            }
        }
    }
}
