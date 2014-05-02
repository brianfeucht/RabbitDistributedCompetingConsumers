using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MassTransit;

namespace RabbitPoC
{
    public interface IMessage : CorrelatedBy<Guid>
    {
        DateTime CreatedOn { get; }
        string Text { get; }
    }
}
