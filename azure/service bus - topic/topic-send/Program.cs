using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;
using queue_send;

namespace topic_send
{
    class Program
    {
        private static string _bus_connectionstring = "Endpoint=sb://appnamespace2000.servicebus.windows.net/;SharedAccessKeyName=topicpolicy;SharedAccessKey=b1Y5V79pd2tFk4b+g7MTWytFz61H7gzSccqFhgs3xnI=";
        private static string _topic_name = "apptopic";
        private static ITopicClient _client;

        static async Task Main(string[] args)
        {
            _client = new TopicClient(_bus_connectionstring, _topic_name);

            for(int i=0;i<10;i++)
            {
                Order obj = new Order();
                var _message = new Message(Encoding.UTF8.GetBytes(obj.ToString()));
                Console.WriteLine($"Sending Message : {obj.Id} ");
                await _client.SendAsync(_message);


            }
            Console.ReadKey();
            await _client.CloseAsync();

        }
    }
}
