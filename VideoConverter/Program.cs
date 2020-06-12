using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using VideoConverter.Core;
using System.Linq;
namespace VideoConverter
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "videos",
                                         durable: false,
                                         exclusive: false,
                                         autoDelete: false,
                                         arguments: null);
                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += (model, ea) =>
                    {
                        var body = ea.Body.ToArray();
                        var message = Encoding.UTF8.GetString(body.ToArray());
                        Console.WriteLine(" [x] Received {0}", message);
                        VideoContext.ScaleVideos(message);
                        
                    };
                    channel.BasicConsume(queue: "videos",
                                         autoAck: true,
                                         consumer: consumer);

                    Console.WriteLine(" Press [enter] to exit.");
                    Console.ReadLine();
                }
            }
                        
        }
    }
}
