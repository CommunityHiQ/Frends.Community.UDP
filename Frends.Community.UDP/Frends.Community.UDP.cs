using System;
using System.ComponentModel;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.CSharp; // You can remove this if you don't need dynamic type in .NET Standard frends Tasks
using Newtonsoft.Json.Linq;

#pragma warning disable 1591

namespace Frends.Community.UDP
{
    public static class UdpTasks
    {
        /// <summary>
        /// This is task
        /// Documentation: https://github.com/CommunityHiQ/Frends.Community.UDP
        /// </summary>
        /// <param name="input">What to repeat.</param>
        /// <param name="options">Define if repeated multiple times. </param>
        /// <param name="cancellationToken"></param>
        /// <returns>{string Replication} </returns>
        public async static Task<Result> SendAndReceive(Parameters input, [PropertyTab] Options options, CancellationToken cancellationToken)
        {

            var output = new Result
            {
                Responses = new JArray()
            };

            if (options.Timeout.Equals(null))
                options.Timeout = 10000;

            using (var client = new UdpClient())
            {
                client.Connect(IPAddress.Parse(input.IpAddress), input.Port);

                foreach (var cmd in input.Commands)
                {
                    var senddata = System.Text.Encoding.ASCII.GetBytes(cmd.SendCommand);

                    await client.SendAsync(senddata, senddata.Length);

                    //Thread.Sleep(100);

                    var timeout = options.Timeout;
                    
                    Task<string> task = Read(client, cancellationToken, timeout, cmd.ResponseStart, cmd.ResponseEnd);

                    if (task.Wait(timeout, cancellationToken))
                    {
                        await task;
                        output.Responses.Add(task.Result);
                    }
                    else

                        throw new TimeoutException();
                }
                client.Close();
                
            }

            return output;
        }

        private static async Task<string> Read(UdpClient client, CancellationToken cancellationToken, int timeout, string start = "", string end = "" )
        {
            string result = "";
            var ctsInternal = new CancellationTokenSource(timeout);

            while (true)
            {
                cancellationToken.ThrowIfCancellationRequested();
                ctsInternal.Token.ThrowIfCancellationRequested();

                var response = await client.ReceiveAsync();                 
                var responseData = System.Text.Encoding.ASCII.GetString(response.Buffer, 0, response.Buffer.Length);
                result += responseData;

                //do we have data to check
                if (result != "")
                {
                    //no start or end checks, so just return data
                    if (start == "" && end == "")
                        return result;

                    //only start tag and was it found ?
                    if (end == "" && result.Contains(start))
                        return result.Substring(result.IndexOf(start));

                    //only end tag and was it found ?
                    if (start == "" && result.Contains(end))
                        return result.Substring(0, (result.IndexOf(end) + end.Length));

                    //both start and end and did we find them ?
                    if (result.Contains(start))
                    {
                        var startIndex = result.IndexOf(start);

                        if (result.IndexOf(end, startIndex) > -1)
                        {
                            var length = result.IndexOf(end, startIndex) - startIndex + end.Length;
                            return result.Substring(startIndex, length);
                        }
                    }
                }              

            }

        }
    }
}
