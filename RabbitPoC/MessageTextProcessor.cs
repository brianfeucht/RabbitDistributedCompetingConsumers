using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using MassTransit;

namespace RabbitPoC
{
    public class MessageTextProcessor : Consumes<IMessage>.Context
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(MessageTextProcessor));

        public void Consume(IConsumeContext<IMessage> message)
        {
            Console.WriteLine("Recieved: {0}", message.Message.Text);
        }
    }
}
