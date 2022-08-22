using System;
using System.Xml.Serialization;

namespace Company3.API.Models
{
    [XmlRoot(ElementName = "xml")]
    public class Response
    {
        [XmlElement(ElementName = "quote")]
        public int Quote { get; set; } = new Random().Next(7000);
    }
}

