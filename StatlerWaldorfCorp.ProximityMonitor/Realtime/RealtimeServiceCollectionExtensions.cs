using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PubnubApi;

namespace StatlerWaldorfCorp.ES_CQRS_ProximityMonitor.Realtime
{
    public static class RealtimeServiceCollectionExtensions
    {
        /// <summary>
        /// A factory method is used to register the Pubnub instance. 
        /// This method retrieves the PubnubFactory from the DI container and uses it to create a Pubnub instance.
        /// </summary>
        public static IServiceCollection AddRealtimeService(this IServiceCollection services)
        {
            // Register PubnubFactory as a singleton
            services.AddTransient<PubnubFactory>();

            // Register Pubnub using a factory method
            services.AddSingleton<Pubnub>(serviceProvider =>
            {
                var factory = serviceProvider.GetRequiredService<PubnubFactory>();
                return factory.CreatePubnub();
            });

            return services;
        }
    }
}