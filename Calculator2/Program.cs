using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Dapr.Client;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Https;
using Microsoft.Extensions.Hosting;

namespace Calculator
{
    public class Program
    {
        #region Members

        private const String _daprHttpPort = "DAPR_HTTP_PORT";
        private const String _defaultPort = "3500";
        private static String _certificateContents = String.Empty;

        private static String _secret = String.Empty;

        #endregion

        #region Methods

        #region Private Methods

        private static void SetupKestrel(HttpsConnectionAdapterOptions config)
        {
            Byte[] bytes = Convert.FromBase64String(_certificateContents);
            Char[] pw = _secret.ToCharArray();

            ReadOnlySpan<Byte> certContents = new(bytes);
            ReadOnlySpan<Char> passwordContents = new(pw);

            X509Certificate2 cert = new(certContents, passwordContents);
            config.ServerCertificate = cert;
        }

        #endregion

        #region Public Methods

        public static async Task Main(String[] args)
        {
            String port = Environment.GetEnvironmentVariable(_daprHttpPort) == null
                ? _defaultPort
                : Environment.GetEnvironmentVariable(_daprHttpPort);

            Boolean isStandalone = Environment.GetEnvironmentVariable("STANDALONE") == null
                ? false
                : Boolean.Parse(Environment.GetEnvironmentVariable("STANDALONE"));

            Dictionary<String, String> secretValues = null;

            if (isStandalone)
            {
                Console.WriteLine("Using Standalone");
                HttpClient httpClient = new();
                try
                {
                    using (var response = await httpClient.GetAsync($"http://localhost:{port}/v1.0/secrets/kubernetes/cert-secret-store"))
                    {
                        secretValues = await response.Content.ReadAsAsync<Dictionary<String, String>>();
                        _secret = secretValues["cert-password"];
                        _certificateContents = secretValues["cert-contents"];
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            else
            {
                Console.WriteLine("Using Dapr");
                DaprClient client = new DaprClientBuilder()
                    .UseHttpEndpoint($"http://localhost:{port}")
                    .Build();

                try
                {
                    secretValues = await client.GetSecretAsync("kubernetes",
                        "cert-secret-store",
                        new Dictionary<String, String> { { "namespace", "default" } }
                    );
                    _secret = secretValues["cert-password"];
                    _certificateContents = secretValues["cert-contents"];
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            await CreateHostBuilder(args).Build().RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(String[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseKestrel(options => { options.ConfigureHttpsDefaults(SetupKestrel); });

                    webBuilder.UseStartup<Startup>();
                });
        }

#endregion

#endregion
    }
}