using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using MassTransit;

namespace RabbitPoC
{
    public class TimeToProcessConsumer : Consumes<IMessage>.Context
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(TimeToProcessConsumer));

        public void Consume(IConsumeContext<IMessage> message)
        {
            var timeToProcess =DateTime.Now - message.Message.CreatedOn;

            Console.WriteLine("Message {0} took {1}ms", message.Message.CorrelationId, timeToProcess.TotalMilliseconds);
        }
    }
}
