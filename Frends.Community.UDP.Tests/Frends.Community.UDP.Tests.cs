using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System;
using System.Threading;

namespace Frends.Community.UDP.Tests
{
    [TestFixture]
    class UDPTests
    {
        private Thread listenerThread;

        [OneTimeSetUp]
        public void StartListener()
        {

            listenerThread = new Thread(new ThreadStart(UDPListener.Listener));
            listenerThread.Start();

        }

        [Test]
        public void TestSendAndReceive()
        {
            CancellationTokenSource source = new CancellationTokenSource();
            CancellationToken token = source.Token;

            var input = new Parameters
            {
                Commands = new Command[] { new Command { SendCommand = "MODEL", ResponseStart = "ACK", ResponseEnd = "\r" },
                new Command{SendCommand = "COMMAND2", ResponseStart = "ACK", ResponseEnd = "" },
                new Command{SendCommand = "COMMAND3", ResponseStart = "ACK", ResponseEnd = "\r" }},
                IpAddress = "127.0.0.1",
                Port = 15000
            };

            var options = new Options
            {
                Timeout = 60000,

            };

            var input2 = new Parameters
            {
                Commands = new Command[] { new Command { SendCommand = "COMMAND1" }, new Command { SendCommand = "" },
                    new Command {SendCommand = "SEND_EMPTY_RESP" }, new Command {SendCommand ="STOP" } },
                IpAddress = "127.0.0.1",
                Port = 15000
            };

            var options2 = new Options
            {
                Timeout = 20000
            };

            var res1 = UdpTasks.SendAndReceive(input, options, token).Result.Responses;
            JArray expected = JArray.Parse(@"['ACK MODEL unAX2IO+\r','ACK ','ACK GETCHNLLABEL RX 2 RXCHNL1\r' ]");
            Assert.AreEqual(expected.ToString(), res1.ToString());
            Assert.That(async () => await UdpTasks.SendAndReceive(input2, options2, token), Throws.Exception);

        }

    }
}
