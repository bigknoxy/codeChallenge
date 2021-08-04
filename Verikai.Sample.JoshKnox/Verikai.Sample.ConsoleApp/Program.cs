using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Verikai.Sample.Service.Models;
using Verikai.Sample.Service.Registration;
using Verikai.Sample.Service.Services.Abstractions;

namespace Verikai.Sample.ConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddServiceLayer();
            var serviceProvider = serviceCollection.BuildServiceProvider();

            Console.WriteLine("About to download file");
            var downloadFileService = serviceProvider.GetRequiredService<IDownloadFileService>();
            await downloadFileService.DownloadFile("http://devchallenge.verikai.com/data.tsv").ConfigureAwait(false);
            Console.WriteLine("File downloaded!");

            Console.WriteLine("Reading file into memory...");
            var fileService = serviceProvider.GetRequiredService<IFileService>();
            var resultFromFile = fileService.ReadFile<Person>("verikaiFile.tsv");

            Console.WriteLine("Mapping to new format...");
            var mappingService = serviceProvider.GetRequiredService<IMappingService>();
            var mappedRecords = mappingService.MapPersonRecords(resultFromFile);

            Console.WriteLine("Writing unencrypted file...");
            fileService.WriteFile<PersonOutput>("unencrypted.tsv", mappedRecords,true);

            Console.WriteLine("Writing encrypted file...");
            await fileService.WriteFileEncryptedAsync<PersonOutput>("encrypted.tsv", mappedRecords, true).ConfigureAwait(false);
            
        }


        //NOTE: Should this be a host/service? does this need to be scheduled?  Watch a file location? Run on demand? 
        //static async Task Main(string[] args)
        //{
        //    using IHost host = CreateHostBuilder(args).Build();

        //    await host.RunAsync();
        //}

        //public static IHostBuilder CreateHostBuilder(string[] args) =>
        //    Host.CreateDefaultBuilder(args)
        //        .ConfigureAppConfiguration((hostingContext, configuration) =>
        //        {
        //            //configuration.Sources.Clear();

        //            IHostEnvironment env = hostingContext.HostingEnvironment;

        //            configuration
        //                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
        //                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true, true);

        //            IConfigurationRoot configurationRoot = configuration.Build();

        //            AppSettings.AppSettings options = new();
        //            configurationRoot.GetSection(nameof(AppSettings.AppSettings))
        //                             .Bind(options);

        //            Console.WriteLine($"AppSettings.FileToDownloadUrl={options.FileToDownloadUrl}");

        //        })
        //    //todo:  Add logging?  Do we need to log this anywhere?
        //    .ConfigureServices((context , services) =>
        //    {
        //        services.AddOptions().Configure<AppSettings.AppSettings>(options =>
        //        context.Configuration.GetSection(nameof(AppSettings.AppSettings)).Bind(options));
        //    });
    }
}
