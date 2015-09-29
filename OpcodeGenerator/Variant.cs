using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace OpcodeGenerator
{
    [Serializable]
    public class Variant
    {
        #region XML

        [XmlAttribute]
        public string Opcode { get; set; }

        [XmlAttribute]
        public string Operand1 { get; set; }

        [XmlAttribute]
        public string Operand2 { get; set; }

        [XmlAttribute]
        public string Operand3 { get; set; }

        [XmlAttribute]
        public string Operand4 { get; set; }

        [XmlAttribute]
        public string Prefix { get; set; }

        [XmlAttribute]
        public string Prefix0F { get; set; }

        [XmlAttribute]
        public int Add { get; set; }

        [XmlAttribute]
        public string FixedReg { get; set; }

        [XmlAttribute]
        public string Mode { get; set; }

        [XmlAttribute]
        public string Lock { get; set; }

        [XmlAttribute]
        public string Group { get; set; }

        [XmlAttribute]
        public string Proc { get; set; }

        [XmlAttribute]
        public string Ring { get; set; }

        #endregion

        string GetOpcodeBytes()
        {
            return "new byte[] { "
                + string.Join(", ", Opcode.Split(' ').Select(s => "0x" + s)) 
                + " }";
        }

        IEnumerable<string> GetArrVariants()
        {
            yield break;
        }

        public string CreateFunctions()
        {
            return GetOpcodeBytes();
        }

        public string CreateVariants()
        {
            return GetOpcodeBytes();
        }
    }
}
