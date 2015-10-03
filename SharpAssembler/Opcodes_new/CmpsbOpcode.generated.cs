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
    /// The CMPSB (Compare Strings) instruction opcode.
    /// </summary>
    public class CmpsbOpcode : X86Opcode
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="CmpsbOpcode"/> class.
        /// </summary>
        public CmpsbOpcode()
            : base("cmpsb", GetOpcodeVariants())
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
                // CMPSB
                new X86OpcodeVariant(
                    new byte[] { 0xA6 }, DataSize.Bit8),
            };
        }
    }
}

namespace SharpAssembler.Architectures.X86
{
    partial class Instr
    {
        /// <summary>
        /// Creates a new CMPSB (Compare Strings) instruction.
        /// </summary>
        /// <returns>The created instruction.</returns>
        public static X86Instruction Cmpsb()
        {
            return X86Opcode.Cmpsb.CreateInstruction();
        }
    }

    partial class X86Opcode
    {
        /// <summary>
        /// The CMPSB (Compare Strings) instruction opcode.
        /// </summary>
        public static readonly X86Opcode Cmpsb = new CmpsbOpcode();
    }
}

//////////////////////////////////////////////////////
//                     WARNING                      //
//     The contents of this file is generated.      //
//    DO NOT MODIFY, your changes will be lost!     //
//////////////////////////////////////////////////////
