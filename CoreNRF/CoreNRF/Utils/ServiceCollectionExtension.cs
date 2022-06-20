using System;
using CoreNRF.Adapters.LocationAdapter;
using CoreNRF.Adapters.NFAdapter;
using CoreNRF.Adapters.ServiceAdapter;
using CoreNRF.Services.LocationService;
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
            services.AddTransient<ILocationService, LocationService>();
            services.AddTransient<INFService, NFService>();
            services.AddTransient<IServicesService, ServicesService>();
            services.AddTransient<ILocationAdapter, LocationAdapter>();
            services.AddTransient<IServiceAdapter, ServiceAdapter>();
            services.AddTransient<INFAdapter, NFAdapter>();
        }
    }
}
