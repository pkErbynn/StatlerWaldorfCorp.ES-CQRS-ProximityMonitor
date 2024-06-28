using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PubnubApi;

namespace StatlerWaldorfCorp.ES_CQRS_ProximityMonitor.Realtime
{
    public class PubnubRealtimePublisher : IRealtimePublisher
    {
        private ILogger logger;
        private Pubnub pubnubClient;

        public PubnubRealtimePublisher(ILogger<PubnubRealtimePublisher> logger, Pubnub pubnubClient)
        {
            this.logger = logger;
            this.pubnubClient = pubnubClient;
        }

        /// <summary>
        /// This calls the Time method on the pubnubClient instance of the Pubnub class. 
        /// This method is used to get the current time from the Pubnub servers to check if its up or down
        /// </summary>
        public async Task ValidateAsync()
        {
             var result = await pubnubClient.Time().ExecuteAsync();
            if (result.Status.Error)
            {
                logger.LogError($"Unable to connect to Pubnub: {result.Status.ErrorData.Information}");
                throw result.Status.ErrorData.Throwable;
            }
            else
            {
                logger.LogInformation("Pubnub connection established.");
            }
        }

        public async Task PublishAsync(string channelName, string message)
        {
            PNResult<PNPublishResult> publishResponse = await pubnubClient.Publish()
                                                                        .Channel(channelName)
                                                                        .Message(message)
                                                                        .ExecuteAsync();
            PNStatus status = publishResponse.Status;
            if (status.Error) {
                logger.LogError($"Failed to publish on channel {channelName}: {status.ErrorData.Information}");
                return;
            }
            logger.LogInformation($"Published message on channel {channelName}, {status.AffectedChannels.Count} affected channels, message: {message}, code: {status.StatusCode}");
        }
    }
}