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

        public AMQPOptionSettings amqpOptions;
        public object _lock;
        public AMQPConnectionFactory(
            ILogger<AMQPConnectionFactory> logger,
            IOptions<AMQPOptionSettings> options)
        {
            this.connectionFactory = new ConnectionFactory
            {
                UserName = amqpOptions.Username,
                Password = amqpOptions.Password,
                VirtualHost = amqpOptions.VirtualHost,
                HostName = amqpOptions.HostName,
                Uri = new Uri(amqpOptions.Uri)
            };

            logger.LogInformation($"AMQP Connection configured for URI : {amqpOptions.Uri}");
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