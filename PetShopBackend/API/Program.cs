using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data.DataAccess;
using API.Data.DataAccess.UnitOfWork;
using API.Data.utils;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();//.Run();

            using var scope = host.Services.CreateScope();

            var services = scope.ServiceProvider;

            //the point of this scope is to set up things that the
            //dependency injection system might depend on. this is
            //the last point of our control before the di system goes online. 
            try
            {
                var context = services.GetRequiredService<DataContext>();
                var uow = services.GetRequiredService<UoW>();
                

                //migrates the db if entire db doesn't exist:
                await context.Database.MigrateAsync();

                await Seed.SeedAnimals(uow);
            }

            catch(System.Exception ex)
            {
                               

                //victor added it to the logger service he added: 
                //fo me this might not do anything, since  ihave yet to 
                //implement this is any way. 

                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex,"An error occured during migration");
            }

            await host.RunAsync();
            
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
