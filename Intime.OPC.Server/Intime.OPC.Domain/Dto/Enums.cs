
using  System.Xml.Serialization;

namespace Intime.OPC.Domain.Dto
{
  
    public class Item
    {

        [XmlAttribute("Key")]
        public string Key { get; set; }

        [XmlAttribute("Value")]
        public string Value { get; set; }
    }
}