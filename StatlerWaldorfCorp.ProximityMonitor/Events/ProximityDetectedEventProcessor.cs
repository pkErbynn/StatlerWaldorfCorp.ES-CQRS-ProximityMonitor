using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using StatlerWaldorfCorp.ES_CQRS_ProximityMonitor.Queues;
using StatlerWaldorfCorp.ES_CQRS_ProximityMonitor.Realtime;
using StatlerWaldorfCorp.ES_CQRS_ProximityMonitor.TeamService;

namespace StatlerWaldorfCorp.ProximityMonitor.Events
{
    public class ProximityDetectedEventProcessor: IEventProcessor
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
            IOptions<PubnubOptions> pubsubOptions)
        {
            this.logger = logger;
            this.publisher = publisher;
            this.subscriber = substriber;
            this.pubnubOptions = pubnubOptions;

            this.logger.LogInformation ("Proximity Event Process instance created...");

            subscriber.ProximityDetectedEventReceived += (proximityDetectedEvent) => {
                Team team = teamServiceClient.GetTeam(proximityDetectedEvent.TeamId);
                Member sourceMember = teamServiceClient.GetMember(proximityDetectedEvent.TeamId, proximityDetectedEvent.SourceMemberId);
                Member targetMember = teamServiceClient.GetMember(proximityDetectedEvent.TeamId, proximityDetectedEvent.TargetMemberId);
            };
        }

        public void start()
        {
            throw new NotImplementedException();
        }

        public void stop()
        {
            throw new NotImplementedException();
        }
    }
}