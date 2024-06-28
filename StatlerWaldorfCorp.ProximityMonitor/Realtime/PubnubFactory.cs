using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using PubnubApi;

namespace StatlerWaldorfCorp.ES_CQRS_ProximityMonitor.Realtime
{
    public class PubnubFactory
    {
        private ILogger logger;
        private PNConfiguration pnConfiguration;

        public PubnubFactory(IOptions<PubnubOptionSettings> pubnubOptionSettings, ILogger<PubnubFactory> logger)
        {
            this.logger = logger;

            this.pnConfiguration = new PNConfiguration(new UserId(pubnubOptionSettings.Value.UserId));
            this.pnConfiguration.SubscribeKey = pubnubOptionSettings.Value.SubscribeKey;
            this.pnConfiguration.PublishKey = pubnubOptionSettings.Value.PublishKey;
            this.pnConfiguration.Secure = false;

            this.logger.LogInformation($"Pubnub Factory using publish key {pnConfiguration.PublishKey}");
        }

        public Pubnub CreatePubnub()
        {
            return new Pubnub(this.pnConfiguration);
        }
    }
}