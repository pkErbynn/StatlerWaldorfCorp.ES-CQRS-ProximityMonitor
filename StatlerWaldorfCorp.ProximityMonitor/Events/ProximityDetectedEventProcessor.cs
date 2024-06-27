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

                ProximityDetectedRealtimeEvent proximityDetectedRealtimeEvent = new ProximityDetectedRealtimeEvent
                {
                    TargetMemberId = proximityDetectedEvent.TargetMemberId,
                    SourceMemberId = proximityDetectedEvent.SourceMemberId,
                    DetectionTime = proximityDetectedEvent.DetectionTime,
                    SourceMemberLocation = proximityDetectedEvent.SourceMemberLocation,
                    TargetMemberLocation = proximityDetectedEvent.TargetMemberLocation,
                    MemberDistance = proximityDetectedEvent.MemberDistance,
                    TeamId = proximityDetectedEvent.TeamId,
                    TeamName = team.Name,
                    SourceMemberName = $"{sourceMember.FirstName} {sourceMember.LastName}",
                    TargetMemberName = $"{targetMember.FirstName} {targetMember.LastName}",
                };

                publisher.Publish(this.pubnubOptions.ProximityEventChannel, proximityDetectedRealtimeEvent.ToJson());
            };
        }

        public void start()
        {
            subscriber.Subscribe();
        }

        public void stop()
        {
            subscriber.Unsubscribe();
        }
    }
}