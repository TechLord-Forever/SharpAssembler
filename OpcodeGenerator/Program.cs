using System;
using System.IO;
using System.Text;

namespace OpcodeGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            if (!Directory.Exists("Opcodes"))
                Directory.CreateDirectory("Opcodes");

            var table = XmlManager.Load<OpcodeTable>("definitions.xml");

            foreach (var opcode in table.OpcodeList)
            {
                Console.WriteLine("Opcode: " + opcode.Name);
                using (var writer = new StreamWriter("Opcodes\\" + opcode.FileName, false, Encoding.UTF8))
                {
                    var content = opcode.GetContent();
                    writer.Write(content);
                }
            }

            //Console.ReadLine();
        }
    }
}
