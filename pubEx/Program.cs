using System;
using System.Diagnostics;
using System.Threading;
using Message;
using NetMQ;
using NetMQ.Sockets;

namespace pubEx
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var pubSocket = new PublisherSocket())

            {
                using (var subSocket = new SubscriberSocket())
                {

                    pubSocket.Bind("tcp://127.0.0.1:55555");

                    var msg = new BlockedOrderCreated();

                  //  pubSocket.SendMoreFrame("BlockedOrderTopic").SendFrame(msg.content);
                  
                    Thread.Sleep(500);

                    subSocket.Connect("tcp://127.0.0.1:55555");
                    Thread.Sleep(500);
                    subSocket.Subscribe("BlockedOrderTopic");
                    pubSocket.SendMoreFrame("BlockedOrderTopic").SendFrame(msg.content);
                    while (true)
                    {


                        Thread.Sleep(500);
                        var messageReceived = subSocket.ReceiveFrameString();
                        Thread.Sleep(500);
                       Debug.WriteLine(messageReceived);
                    }


                }


                 
            }
        }
    }
}
