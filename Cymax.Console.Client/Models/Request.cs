using System;
namespace Cymax.Console.Client.Models
{
    /// <summary>
    /// Class that represents all Cymax request to the All suppliers.
    /// </summary>
    public class Request
    {
        public string SourceAddress { get; set; }
        public string DestinationAddress { get; set; }
        public int[] CartonDimensions { get; set; }
    }
}

