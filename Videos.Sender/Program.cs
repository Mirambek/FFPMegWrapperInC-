using RabbitMQ.Client;
using System;
using System.Text;

namespace Videos.Sender
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "videos",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);
                foreach(var s in new String[1] {"http://techslides.com/demos/sample-videos/small.mp4"})
				{
					string message = s;
					var body = Encoding.UTF8.GetBytes(message);

					channel.BasicPublish(exchange: "",
                                     routingKey: "videos",
                                     basicProperties: null,
                                     body: body);
					Console.WriteLine(" [x] Sent {0}", message);
				}
            }

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }
    }
}
