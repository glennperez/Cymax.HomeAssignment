using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace Company3.API.Models
{
    [XmlRoot(ElementName ="root")]
    public class Bid
    {
        [Required]
        [XmlElement(ElementName = "source")]
        public string Source { get; set; }

        [Required]
        [XmlElement(ElementName = "destination")]
        public string Destination { get; set; }

        [Required]
        [XmlArray("packages")]
        [XmlArrayItem(ElementName = "package")]
        public List<int> Packages { get; set; }
    }
}

