using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StatlerWaldorfCorp.ProximityMonitor.Events;

namespace StatlerWaldorfCorp.ES_CQRS_ProximityMonitor.TeamService
{
    public class ProximityDetectedRealtimeEvent: ProximityDetectedEvent
    {
        public string TeamName { get; set; }
        public string SourceMemberName { get; set;}
        public string TargetMemberName { get; set;}
    }
}