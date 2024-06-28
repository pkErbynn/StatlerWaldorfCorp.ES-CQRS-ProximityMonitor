using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using StatlerWaldorfCorp.ProximityMonitor.Events;

namespace StatlerWaldorfCorp.ES_CQRS_ProximityMonitor.Queues
{
    public class RabbitMQEventSubscriber : IEventSubscriber
    {
        private QueueOptionSettings queueSettingOptions;
        private ILogger logger;
        private EventingBasicConsumer consumer;
        private AMQPConnectionFactory connectionFactory;
        private IModel channel;
        private string consumerTag;

        public event ProximityDetectedEventReceivedDelegate ProximityDetectedEventReceived;

        public RabbitMQEventSubscriber(
            ILogger<RabbitMQEventSubscriber> logger,
            IOptions<QueueOptionSettings> queueSettings,
            EventingBasicConsumer consumer,
            AMQPConnectionFactory aMQPConnectionFactory)
        {
            this.logger = logger;
            this.queueSettingOptions = queueSettings.Value;
            this.consumer = consumer;
            this.channel = consumer.Model;
            this.connectionFactory = aMQPConnectionFactory;

            logger.LogInformation("Created RabbitMQ Event subscriber instance");

            Initialize();
        }

        private void Initialize()
        {
            this.channel.QueueDeclare(
                queue: this.queueSettingOptions.ProximityDetectedEventQueueName,
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null
            );
            this.consumer.Received += (channel, eventArgs) => {
                var body = eventArgs.Body;
                var message = Encoding.UTF8.GetString(body.ToArray());

                var proximityDetectedEvent = JsonConvert.DeserializeObject<ProximityDetectedEvent>(message);
                logger.LogInformation($"Received incoming event, {body.Length} bytes.");
                
                if(ProximityDetectedEventReceived != null)
                {
                    ProximityDetectedEventReceived(proximityDetectedEvent);
                }

                this.channel.BasicAck(eventArgs.DeliveryTag, false);
            };
        }

        public void Subscribe()
        {
            this.consumerTag = channel.BasicConsume(this.queueSettingOptions.ProximityDetectedEventQueueName, false, this.consumer);
            logger.LogInformation($"Subscribed to queue {this.queueSettingOptions.ProximityDetectedEventQueueName}, consumerTag = {consumerTag}");
        }

        public void Unsubscribe()
        {
            this.channel.BasicCancel(this.consumerTag);
            logger.LogInformation($"Stopped subscription on queue {this.queueSettingOptions.ProximityDetectedEventQueueName}");
        }
    }
}