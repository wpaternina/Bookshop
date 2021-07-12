using Api.Models;
using Api.Persistences;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;

namespace Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        //public static void Main(string[] args)
        //{
        //    var hostserver = CreateHostBuilder(args).Build();
        //    using (var ambiente = hostserver.Services.CreateScope())
        //    {
        //        var services = ambiente.ServiceProvider;

        //        try
        //        {
        //            //var userManager = services.GetRequiredService<UserManager<Usuario>>();

        //            var context = services.GetRequiredService<BookshopContext>();
        //            context.Database.Migrate();

        //            //DataPrimerUsuario.InsertarData(context, userManager).Wait();

        //        }
        //        catch (Exception ex)
        //        {
        //            var logging = services.GetRequiredService<ILogger<Program>>();
        //            logging.LogError(ex, "Ocurrio un error en la migración");
        //        }
        //    }
        //    hostserver.Run();
        //}

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
