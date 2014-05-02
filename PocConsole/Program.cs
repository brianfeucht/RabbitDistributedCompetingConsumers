using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitPoC;

namespace PocConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("What To Publish?");

            var line = Console.ReadLine();

            var messageBus = new MessageProcessService();
            messageBus.StartConsumer();

            while (!string.IsNullOrWhiteSpace(line))
            {
                var message = new Message(line);
                messageBus.PublishMessage(message);

                line = Console.ReadLine();
            }
        }
    }
}
