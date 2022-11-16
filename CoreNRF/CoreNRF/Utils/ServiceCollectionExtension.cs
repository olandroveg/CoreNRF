using System;
using CoreNRF.Adapters.LocationAdapter;
using CoreNRF.Adapters.NFAdapter;
using CoreNRF.Adapters.NFServAdapter;
using CoreNRF.Adapters.PortalNfAdapter;
using CoreNRF.Adapters.ServiceAdapter;
using CoreNRF.Models;
using CoreNRF.Services.LocationService;
using CoreNRF.Services.NFService;
using CoreNRF.Services.NFServService;
using CoreNRF.Services.PortalNFService;
using CoreNRF.Services.PortalService;
using CoreNRF.Services.ServicesService;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CoreNRF.Utils
{
    public static class ServiceCollectionExtension
    {
        public static void UseInjection(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<INFServService, NFServService>();
            services.AddTransient<ILocationService, LocationService>();
            services.AddTransient<INFService, NFService>();
            services.AddTransient<IServicesService, ServicesService>();
            services.AddTransient<IPortalService, PortalService>();
            services.AddTransient<IPortalNFService, PortalNFService>();
            services.AddTransient<ILocationAdapter, LocationAdapter>();
            services.AddTransient<IServiceAdapter, ServiceAdapter>();
            services.AddTransient<INFAdapter, NFAdapter>();
            services.AddTransient<INFServAdapter, NFServAdapter>();
            services.AddTransient<IPortalNfAdapter, PortalNfAdapter>();
        }
    }
}
