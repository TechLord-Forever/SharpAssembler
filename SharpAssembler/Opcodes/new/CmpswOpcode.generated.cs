//////////////////////////////////////////////////////
//                     WARNING                      //
//     The contents of this file is generated.      //
//    DO NOT MODIFY, your changes will be lost!     //
//////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using SharpAssembler.Architectures.X86.Opcodes;
using SharpAssembler.Architectures.X86.Operands;

namespace SharpAssembler.Architectures.X86.Opcodes
{
    /// <summary>
    /// The CMPSW (Compare Strings) instruction opcode.
    /// </summary>
    public class CmpswOpcode : X86Opcode
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="CmpswOpcode"/> class.
        /// </summary>
        public CmpswOpcode()
            : base("cmpsw", GetOpcodeVariants())
        {
        }
        #endregion

        /// <summary>
        /// Returns the opcode variants of this opcode.
        /// </summary>
        /// <returns>An enumerable collection of <see cref="X86OpcodeVariant"/> objects.</returns>
        private static IEnumerable<X86OpcodeVariant> GetOpcodeVariants()
        {
            return new X86OpcodeVariant[]{
                // CMPSW
                new X86OpcodeVariant(
                    new byte[] { 0xA7 }, DataSize.Bit16),
            };
        }
    }
}

namespace SharpAssembler.Architectures.X86
{
    partial class Instr
    {
        /// <summary>
        /// Creates a new CMPSW (Compare Strings) instruction.
        /// </summary>
        /// <returns>The created instruction.</returns>
        public static X86Instruction Cmpsw()
        {
            return X86Opcode.Cmpsw.CreateInstruction();
        }
    }

    partial class X86Opcode
    {
        /// <summary>
        /// The CMPSW (Compare Strings) instruction opcode.
        /// </summary>
        public static readonly X86Opcode Cmpsw = new CmpswOpcode();
    }
}

//////////////////////////////////////////////////////
//                     WARNING                      //
//     The contents of this file is generated.      //
//    DO NOT MODIFY, your changes will be lost!     //
//////////////////////////////////////////////////////
