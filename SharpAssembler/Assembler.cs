using SharpAssembler.Architectures.X86;
using SharpAssembler.Instructions;
using System;
using System.Collections.Generic;
using System.IO;

namespace SharpAssembler
{
    partial class Assembler { /* stuff */ }

    /// <summary>
    ///
    /// </summary>
    public partial class Assembler
    {
        List<EncodedInstruction> instructions = new List<EncodedInstruction>();

        /// <summary>
        ///
        /// </summary>
        public Context Context { get; private set; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="addressingMode"></param>
        public Assembler(DataSize addressingMode)
            :this(addressingMode, 0UL, false)
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="addressingMode"></param>
        /// <param name="offset"></param>
        [CLSCompliant(false)]
        public Assembler(DataSize addressingMode, ulong offset)
            : this(addressingMode, offset, false)
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="addressingMode"></param>
        /// <param name="offset"></param>
        /// <param name="useRipRelative"></param>
        [CLSCompliant(false)]
        public Assembler(DataSize addressingMode, ulong offset, bool useRipRelative)
        {
            Context = new Context(addressingMode, offset, useRipRelative);
        }

        void EmmitInstruction()
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="identifier"></param>
        /// <returns></returns>
        public Label DefineLabel(string identifier = null)
        {
            return new Label(identifier);
        }

        /// <summary>
        ///
        /// </summary>
        public void Reset()
        {
            instructions.Clear();
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public byte[] Assemble()
        {
            using (var ms = new MemoryStream())
            {
                using (var writer = new BinaryWriter(ms))
                {
                    foreach (var instruction in instructions)
                    {
                        instruction.Emit(writer, Context);
                    }
                }
                return ms.ToArray();
            }
        }
    }
}
