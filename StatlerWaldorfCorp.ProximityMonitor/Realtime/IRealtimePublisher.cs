using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StatlerWaldorfCorp.ES_CQRS_ProximityMonitor.Realtime
{
    public interface IRealtimePublisher
    {
        Task PublishAsync(string channelName, string message);
        Task ValidateAsync();
    }
}