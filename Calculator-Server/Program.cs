using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;
using Dapr.Client;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Https;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Core;
using Serilog.Exceptions;

namespace Calculator
{
    public class Program
    {
        #region Members

        private const String _daprHttpPort = "DAPR_HTTP_PORT";
        private const String _defaultPort = "3500";

        private static String _certificateContents = String.Empty;
        private static Logger _logger;

        private static String _secret = String.Empty;

        #endregion

        #region Methods

        #region Private

        private static void ConfigureWebHost(IWebHostBuilder webBuilder)
        {
            // Configure Kestrel
            _logger.Information("ConfigureKestrel");

            webBuilder.UseKestrel(options =>
            {
                _logger.Information("ConfigureHttpsDefaults");
                options.ConfigureHttpsDefaults(SetupKestrel);
            });

            // Startup to configure services Certificates depend on

            webBuilder.UseStartup<Startup>();
        }


        private static async Task GetCertificateDetails()
        {
            String port = Environment.GetEnvironmentVariable(_daprHttpPort) == null
                ? _defaultPort
                : Environment.GetEnvironmentVariable(_daprHttpPort);

            _logger.Information("Loading Certificates from Secret Store");

            DaprClient client = new DaprClientBuilder()
                .UseHttpEndpoint($"http://localhost:{port}")
                .Build();

            try
            {
                Dictionary<String, String> secretValues = await client.GetSecretAsync("CertSecretStore",
                    "cert-localhost",
                    new Dictionary<String, String> { { "namespace", "default" } }
                );
                _secret = secretValues["password"];
                _certificateContents = secretValues["certificate"];

                _logger.Information($"Loaded Certificate : {_certificateContents}");
            }
            catch (Exception e)
            {
                _logger.Error(e, String.Empty);
            }
        }


        private static void SetupKestrel(HttpsConnectionAdapterOptions config)
        {
            _logger.Information("SetupKestrel");

            config.ServerCertificateSelector = (c, n) =>
            {
                if (String.IsNullOrWhiteSpace(_certificateContents))
                {
                    GetCertificateDetails();
                }

                Int32 tryCount = 0;

                while (String.IsNullOrWhiteSpace(_certificateContents))
                {
                    _logger.Information("Waiting For Certificate To Load");
                    Thread.Sleep(1000);
                    tryCount++;
                    if (tryCount > 30)
                    {
                        throw new OperationException("Unable to load a valid certificate");
                    }
                }

                Byte[] bytes = Convert.FromBase64String(_certificateContents);

                Char[] pw = _secret.ToCharArray();

                ReadOnlySpan<Byte> certContents = new(bytes);
                ReadOnlySpan<Char> passwordContents = new(pw);

                X509Certificate2 cert = new(certContents, passwordContents);
                config.ServerCertificate = cert;
                return cert;
            };
        }

        #endregion

        #region Public

        public static IHostBuilder CreateHostBuilder(String[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(ConfigureWebHost);
        }


        public static async Task Main(String[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, true)
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", true, true)
                .Build();

            _logger = new LoggerConfiguration()
                .Enrich.WithExceptionDetails()
                .ReadFrom
                .Configuration(configuration)
                .CreateLogger();

            _logger.Information($"Starting up Calculator Server on host : {Dns.GetHostName()}");

            IHost host = CreateHostBuilder(args)
                .UseSerilog(_logger)
                .Build();

            await host.RunAsync();
        }

        #endregion

        #endregion
    }
}