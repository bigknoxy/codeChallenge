using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verikai.Sample.Service.Services;
using Verikai.Sample.Service.Services.Abstractions;

namespace Verikai.Sample.Service.Registration
{
    public static class RegistrationExtensions
    {
        public static IServiceCollection AddServiceLayer(this IServiceCollection services )
        {
            services.AddTransient<IDownloadFileService>(c => new DownloadFileService());
            services.AddTransient<IFileService>(c => new FileService(c.GetRequiredService<IEncryptionService>()));
            services.AddTransient<IMappingService>(c => new MappingService(c.GetRequiredService<IReferenceData>()));
            services.AddTransient<IReferenceData>(c => new ReferenceData(c.GetRequiredService<IFileService>()));
            services.AddTransient<IEncryptionService>(c => new EncryptionService());
            return services;
        }
    }
}
