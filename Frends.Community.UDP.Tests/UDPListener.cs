using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Frends.Community.UDP.Tests
{
    class UDPListener
    {
        public async static void Listener()
        {
            var address = IPAddress.Parse("127.0.0.1");
            var port = 15000;
            var ipep = new IPEndPoint(address, port);
            var loop = true;

            using (var server = new UdpClient(ipep))
            {
                var sender = new IPEndPoint(IPAddress.Any, port);

                while (loop)
                {
                    var received = server.Receive(ref sender);
                    var data = Encoding.ASCII.GetString(received , 0, received.Length).ToUpper();



                    if (data.Equals("STOP"))
                        break;
                    else if (data.Equals("SEND_EMPTY_RESP"))
                    {
                        var msgEmpty = new byte[0];
                        server.Send(msgEmpty, 0, sender);
                    }
                    else if (data.Equals("MODEL"))
                    {
                        var msg = Encoding.ASCII.GetBytes("ACK MODEL unAX2IO+\r");
                        server.Send(msg, msg.Length, sender);
                    }
                    else
                    {
                        byte[] msg1 = System.Text.Encoding.ASCII.GetBytes("dsummy data");
                        byte[] msg2 = System.Text.Encoding.ASCII.GetBytes("ACK ");
                        byte[] msg3 = System.Text.Encoding.ASCII.GetBytes("GETCHNLLABEL RX 2 RXCHNL1\r");

                         server.Send(msg1, msg1.Length, sender);
                        Thread.Sleep(1000);
                         server.Send(msg2, msg2.Length, sender);
                        Thread.Sleep(1000);
                        server.SendAsync(msg3, msg3.Length,  sender);

                    }

                }
            }
        } 
    }
}