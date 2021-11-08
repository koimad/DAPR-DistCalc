using System;
using System.IO;
using System.Reflection;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Core;

namespace Add
{
    public class Program
    {
        #region Methods

        #region Public Methods

        public static void Main(String[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, true)
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", true, true)
                .Build();

            Logger logger = new LoggerConfiguration()
                .ReadFrom
                .Configuration(configuration)
                .CreateLogger();

            logger.Information("Starting up Add Service");

            CreateHostBuilder(args)
                .UseSerilog(logger)
                .Build()
                .Run();
        }

        public static IHostBuilder CreateHostBuilder(String[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
        }

        #endregion

        #endregion
    }
}