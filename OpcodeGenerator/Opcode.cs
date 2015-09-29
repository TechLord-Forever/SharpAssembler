using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace OpcodeGenerator
{
    [Serializable]
    public class Opcode
    {
        [XmlAttribute]
        public string Name { get; set; }

        [XmlAttribute]
        public string Desc { get; set; }

        [XmlElement("Variant")]
        public List<Variant> VariantList { get; set; }

        StringBuilder content = new StringBuilder();

        public string FileName { get { return $"{Name}_Opcode.cs"; } }

        public string GetContent()
        {
            CreateHeader();
            WriteClassBegin();

            foreach (var variant in VariantList)
                content.AppendLine(variant.CreateVariants());

            WriteClassEnd();

            CreateFunctionsBegin();

            foreach (var variant in VariantList)
                content.AppendLine(variant.CreateFunctions());

            CreateFunctionsEnd();

            CreateFooter();
            return content.ToString();
        }

        void CreateHeader()
        {
            content.AppendLine(
@"//////////////////////////////////////////////////////
//                     WARNING                      //
//     The contents of this file is generated.      //
//    DO NOT MODIFY, your changes will be lost!     //
//////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using SharpAssembler.Architectures.X86.Opcodes;
using SharpAssembler.Architectures.X86.Operands;
");
        }

        void WriteClassBegin()
        {
            content.AppendLine($"namespace SharpAssembler.Architectures.X86.Opcodes");
            content.AppendLine($"{{");
            content.AppendLine($"    /// <summary>");
            content.AppendLine($"    /// {Desc} instruction opcode.");
            content.AppendLine($"    /// </summary>");
            content.AppendLine($"    public class {Name}_Opcode : X86Opcode");
            content.AppendLine($"    {{");
            content.AppendLine($"        /// <summary>");
            content.AppendLine($"        /// Initializes a new instance of the <see cref=\"{Name}_Opcode\"/> class.");
            content.AppendLine($"        /// </summary>");
            content.AppendLine($"        public {Name}_Opcode()");
            content.AppendLine($"            : base(\"{Name.ToLower()}\")");
            content.AppendLine($"        {{");
            content.AppendLine($"        }}");
            content.AppendLine($"");
            content.AppendLine($"        /// <summary>");
            content.AppendLine($"        /// Returns the opcode variants of this opcode.");
            content.AppendLine($"        /// </summary>");
            content.AppendLine($"        Variants = new X86OpcodeVariant[] {{");
        }

        void WriteClassEnd()
        {
            content.AppendLine("        };");
            content.AppendLine("    }");
            content.AppendLine("}");
        }

        void CreateFunctionsBegin()
        {
            content.AppendLine(@"
namespace SharpAssembler.Architectures.X86
{
    partial class Instr
    {");
        }

        void CreateFunctionsEnd()
        {
            content.AppendLine("    }");
        }

        void CreateFooter()
        {
            content.AppendLine($"");
            content.AppendLine($"    partial class X86Opcode");
            content.AppendLine($"    {{");
            content.AppendLine($"        /// <summary>");
            content.AppendLine($"        /// {Desc} instruction opcode.");
            content.AppendLine($"        /// </summary>");
            content.AppendLine($"        public static readonly X86Opcode {Name} = new {Name}_Opcode();");
            content.AppendLine($"    }}");
            content.AppendLine($"}}");
            content.Append(@"
//////////////////////////////////////////////////////
//                     WARNING                      //
//     The contents of this file is generated.      //
//    DO NOT MODIFY, your changes will be lost!     //
//////////////////////////////////////////////////////");
        }
    }
}
