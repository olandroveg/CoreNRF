using System;
using CoreNRF.Services.NFService;
using CoreNRF.Services.ServicesService;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CoreNRF.Utils
{
    public static class ServiceCollectionExtension
    {
        public static void UseInjection(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<INFService, NFService>();
            services.AddTransient<IServicesService, ServicesService>();
        }
    }
}
