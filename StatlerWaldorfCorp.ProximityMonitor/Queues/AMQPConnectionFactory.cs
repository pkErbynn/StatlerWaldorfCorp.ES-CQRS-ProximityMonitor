using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace StatlerWaldorfCorp.ES_CQRS_ProximityMonitor.Queues
{
    /// <summary>
    /// Single Connection Used in multiples Services,
    /// Registered as Signgled and injected in multiple places
    /// </summary> <summary>
    /// 
    /// </summary>
    public class AMQPConnectionFactory
    {
        private ConnectionFactory connectionFactory;
        private IConnection connection;
        private AMQPOptionSettings amqpOptions;
        private readonly object _lock = new object();

        public AMQPConnectionFactory(
            ILogger<AMQPConnectionFactory> logger,
            IOptions<AMQPOptionSettings> amqpOptions)
        {
            this.connectionFactory = new ConnectionFactory
            {
                UserName = amqpOptions.Value.Username,
                Password = amqpOptions.Value.Password,
                VirtualHost = amqpOptions.Value.VirtualHost,
                HostName = amqpOptions.Value.HostName,
                Uri = new Uri(amqpOptions.Value.Uri)
            };

            logger.LogInformation($"AMQP Connection configured for URI : {amqpOptions.Value.Uri}");
        }

        public IConnection GetConnection()
        {
            if (this.connection == null || !this.connection.IsOpen)
            {
                lock (_lock)
                {
                    if (this.connection == null || !this.connection.IsOpen)
                    {
                        this.connection = this.connectionFactory.CreateConnection();
                    }
                }
            }
            return this.connection;
        }
    }
}