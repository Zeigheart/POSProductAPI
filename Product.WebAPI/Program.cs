using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Product.WebAPI.Models;

namespace Product.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args)
        {
            var host=WebHost.CreateDefaultBuilder(args)
                .UseKestrel()
                .UseUrls("http://*:8090")
                .UseStartup<Startup>()
                .UseIISIntegration()
                .Build();
            using (var scope = host.Services.CreateScope())
            {
                //3. Get the instance of BoardGamesDBContext in our services layer
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<POSStoreDbContext>();

                //4. Call the DataGenerator to create sample data
                DataGenerator.Initialize(services);
            }
            return host;
        }
           
    }
}
