using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace StatlerWaldorfCorp.ES_CQRS_ProximityMonitor.Queues
{
    public class RabbitMQEventingConsumer : EventingBasicConsumer
    {
        public RabbitMQEventingConsumer(AMQPConnectionFactory connectionFactory) 
            : base(connectionFactory.GetConnection().CreateModel())
        {
        }
    }
}