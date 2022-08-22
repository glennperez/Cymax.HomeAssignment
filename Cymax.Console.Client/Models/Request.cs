using System;
namespace Cymax.Console.Client.Models
{
    public class Request
    {
        public string SourceAddress { get; set; }
        public string DestinationAddress { get; set; }
        public int[] CartonDimensions { get; set; }
    }
}

