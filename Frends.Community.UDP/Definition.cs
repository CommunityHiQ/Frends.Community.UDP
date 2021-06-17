#pragma warning disable 1591

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json.Linq;

namespace Frends.Community.UDP
{
    /// <summary>
    /// Parameters class usually contains parameters that are required.
    /// </summary>
    public class Parameters
    {
        [DisplayFormat(DataFormatString = "Text")]
        public Command[] Commands { get; set; }

        [DisplayFormat(DataFormatString = "Text")]
        public string IpAddress { get; set; }

        [DisplayFormat(DataFormatString = "Text")]
        [DefaultValue(161)]
        public int Port { get; set; }
    }

    public class Command
    {
        public string SendCommand { get; set; }
        public string ResponseStart { get; set; }
        public string ResponseEnd { get; set; }

    }

    /// <summary>
    /// Options class provides additional optional parameters.
    /// </summary>
    public class Options
    {
        /// <summary>
        /// Task timeout (ms). Operation will timeout in case of empty response.
        /// </summary>
        [DefaultValue(10000)]
        public int Timeout { get; set; }
    }

    public class Result
    {
        /// <summary>
        /// Responses in JArray
        /// </summary>
        [DisplayFormat(DataFormatString = "Text")]
        public JArray Responses;
    }
}
