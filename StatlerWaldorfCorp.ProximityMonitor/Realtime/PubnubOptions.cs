using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StatlerWaldorfCorp.ES_CQRS_ProximityMonitor.Realtime
{
    public class PubnubOptions
    {
        public string PublishKey { get; set; }
        public string SubscribeKey { get; set;}
        public string StartupChannel { get; set;}
        public string ProximityEventChannel { get; set;}
    }
}