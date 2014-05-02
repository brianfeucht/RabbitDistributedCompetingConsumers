using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitPoC;

namespace PocConsole
{
    public class Message : IMessage
    {
        public Guid CorrelationId { get; private set; }
        public DateTime CreatedOn { get; private set; }
        public string Text { get; private set; }

        public Message(string text)
        {
            this.CorrelationId = Guid.NewGuid();
            this.CreatedOn = DateTime.Now;
            this.Text = text;
        }

    }
}
