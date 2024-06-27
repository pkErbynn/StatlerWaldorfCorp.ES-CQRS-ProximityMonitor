using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StatlerWaldorfCorp.ProximityMonitor.Events;

namespace StatlerWaldorfCorp.ES_CQRS_ProximityMonitor.Queues
{
    public delegate void ProximityDetectedEventReceivedDelegate(ProximityDetectedEvent proximityDetectedEvent);
    
    public interface IEventSubscriber
    {
        void Subscribe();
        void Unsubscribe();

        event ProximityDetectedEventReceivedDelegate ProximityDetectedEventReceived;
    }
}