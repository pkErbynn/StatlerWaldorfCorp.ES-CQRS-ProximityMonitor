using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Events
{
    public class ProximityDetectedEventProcessor
    {
        private ILogger logger;
        private IRealtimePublisher publisher;
        private IEventSubscriber subscriber;

        private PubnubOptions pubnubOptions;

        public ProximityDetectedEventProcessor(
            ILogger<ProximityDetectedEventProcessor> logger,
            IRealtimePublisher publisher,
            IEventSubscriber substriber,
            ITeamServiceClient teamServiceClient,
            IOptions<PubsubOptions> pubsubOptions)
        {
            this.logger = logger;
            this.publisher = publisher;
            this.subscriber = substriber;

            this.pubnubOptions = pubnubOptions;

            
        }
    }
}