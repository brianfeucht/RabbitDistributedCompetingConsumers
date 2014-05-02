using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MassTransit;

namespace RabbitPoC
{
    public class MessageProcessService
    {
        private List<IServiceBus> _consumerbusBuses = null;
        private static object lockObject = new object();

        public void StartConsumer()
        {
            if (_consumerbusBuses != null)
            {
                return;
            }

            lock (lockObject)
            {
                if (_consumerbusBuses != null)
                {
                    return;
                }

                _consumerbusBuses = new List<IServiceBus>();

                var messageTextBus = ServiceBusFactory.New(x =>
                {
                    x.UseRabbitMq();
                    x.ReceiveFrom("rabbitmq://localhost/poc_message_messageText_queue");
                });

                messageTextBus.SubscribeConsumer<MessageTextProcessor>();
                _consumerbusBuses.Add(messageTextBus);

                var timeToProcessBus = ServiceBusFactory.New(x =>
                {
                    x.UseRabbitMq();
                    x.ReceiveFrom("rabbitmq://localhost/poc_message_timeToProcess_queue");
                });
                timeToProcessBus.SubscribeConsumer<TimeToProcessConsumer>();
                _consumerbusBuses.Add(timeToProcessBus);
            }
        }

        public void PublishMessage(IMessage message)
        {
            using (var publisherBus = ServiceBusFactory.New(x =>
            {
                x.UseRabbitMq();
                x.ReceiveFrom("rabbitmq://localhost/poc_message");
            }))
            {
                publisherBus.Publish(message);
            }
        }

        public void Stop()
        {
            if (_consumerbusBuses == null)
            {
                return;
            }

            lock (lockObject)
            {
                if (_consumerbusBuses == null)
                {
                    return;
                }

                foreach (var bus in _consumerbusBuses)
                {
                    bus.Dispose();
                }

                _consumerbusBuses = null;
            }
        }
    }
}
