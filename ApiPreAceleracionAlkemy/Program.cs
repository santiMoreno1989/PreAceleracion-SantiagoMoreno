using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Sinks.Elasticsearch;
using System;
using System.Configuration;
using System.IO;
using System.Reflection;

namespace ApiPreAceleracionAlkemy
{
    public class Program
    {
        //public static IConfiguration config { get; } = new ConfigurationBuilder()
        //    .SetBasePath(Directory.GetCurrentDirectory())
        //    .AddJsonFile("appsettings.json", optional:false,reloadOnChange:true)
        //    .AddEnvironmentVariables()
        //    .Build();
        public static void Main(string[] args)
        {
            //var elasticSearchNodeUri=  config["LoggingOptions:NodeUri"];

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json",optional:false,reloadOnChange:true)
                .Build();

            var name= Assembly.GetExecutingAssembly().GetName();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .Enrich.With<CustomEnricher>()
                //.WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(elasticSearchNodeUri))
                //{
                //    AutoRegisterTemplate = true,
                //    IndexFormat = $"My Index - Logs -{DateTime.Now:f}"
                //}
                //)
                .CreateLogger();

            try
            {
                Log.Information("Application Starting up");
                CreateHostBuilder(args).Build().Run();
            }
            catch (System.Exception ex)
            {

                Log.Fatal(ex, "The application failed to start correctly");
            }
            finally 
            {
                Log.CloseAndFlush();
            }

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

    }
}
