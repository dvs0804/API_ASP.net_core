using CityInfo.API.Context;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
            try
            {
                
                logger.Info("Inicializando aplicacion...");
               var host = CreateHostBuilder(args).Build();
                using(var scope = host.Services.CreateScope())
                {
                    try
                    {
                        var context = scope.ServiceProvider.GetService<CityInfoContext>();


                        context.Database.EnsureDeleted();
                        context.Database.Migrate();
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex,"Ha ocurrido un error mientras se migra la base de datos");
                        
                    }
                }
                host.Run();
            }
            catch (Exception ex)
            {

                logger.Error(ex,"Se detuvo la aplicacion por la excepcion.");
                throw;
            }
            finally
            {
                NLog.LogManager.Shutdown();
            }
           
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>()
                    .UseNLog();
                });
    }
}
