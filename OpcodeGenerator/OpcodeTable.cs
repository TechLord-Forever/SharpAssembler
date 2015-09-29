using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace OpcodeGenerator
{
    [Serializable]
    [XmlRoot("OpcodeList")]
    public class OpcodeTable
    {
        [XmlElement("Opcode")]
        public List<Opcode> OpcodeList { get; set; }
    }
}
