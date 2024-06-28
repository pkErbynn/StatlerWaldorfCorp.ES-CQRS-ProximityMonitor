using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PubnubApi;

namespace StatlerWaldorfCorp.ES_CQRS_ProximityMonitor.Realtime
{
    public static class RealtimeServiceCollectionExtensions
    {
        public static IServiceCollection AddRealtimeService(this IServiceCollection services)
        {
            services.AddTransient<PubnubFactory>();
            
            return AddInternal(services, p => p.GetRequiredService<PubnubFactory>(), ServiceLifetime.Singleton);
        }

        private static IServiceCollection AddInternal(
            this IServiceCollection serviceCollection, 
            Func<IServiceProvider, PubnubFactory> factoryProvider,
            ServiceLifetime lifetime)
        {
            Func<IServiceProvider, object> factoryFunc = (provider) => {
                var factory = factoryProvider(provider);
                return factory.CreatePubnub();
            };   

            var descriptor = new ServiceDescriptor(typeof(Pubnub), factoryFunc, lifetime);
            serviceCollection.Add(descriptor);
            return serviceCollection;
        } 
    }
}