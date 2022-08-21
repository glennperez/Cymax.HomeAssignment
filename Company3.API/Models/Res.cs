using System;
using System.Xml.Serialization;

namespace Company3.API.Models
{
    [XmlRoot(ElementName = "xml")]
    public class Res
    {
        private int quote = new Random().Next(7000);

        [XmlElement(ElementName = "quote")]
        public int Quote
        {
            get => quote;
            set { quote = value; }
        }
    }
}

