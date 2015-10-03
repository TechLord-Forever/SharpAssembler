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
    /// The CLD (Clear Direction Flag) instruction opcode.
    /// </summary>
    public class CldOpcode : X86Opcode
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="CldOpcode"/> class.
        /// </summary>
        public CldOpcode()
            : base("cld", GetOpcodeVariants())
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
                // CLD
                new X86OpcodeVariant(
                    new byte[] { 0xFC }),
            };
        }
    }
}

namespace SharpAssembler.Architectures.X86
{
    partial class Instr
    {
        /// <summary>
        /// Creates a new CLD (Clear Direction Flag) instruction.
        /// </summary>
        /// <returns>The created instruction.</returns>
        public static X86Instruction Cld()
        {
            return X86Opcode.Cld.CreateInstruction();
        }
    }

    partial class X86Opcode
    {
        /// <summary>
        /// The CLD (Clear Direction Flag) instruction opcode.
        /// </summary>
        public static readonly X86Opcode Cld = new CldOpcode();
    }
}

//////////////////////////////////////////////////////
//                     WARNING                      //
//     The contents of this file is generated.      //
//    DO NOT MODIFY, your changes will be lost!     //
//////////////////////////////////////////////////////
