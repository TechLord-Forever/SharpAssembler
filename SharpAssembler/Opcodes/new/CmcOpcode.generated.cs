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
    /// The CMC (Complement Carry Flag) instruction opcode.
    /// </summary>
    public class CmcOpcode : X86Opcode
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="CmcOpcode"/> class.
        /// </summary>
        public CmcOpcode()
            : base("cmc", GetOpcodeVariants())
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
                // CMC
                new X86OpcodeVariant(
                    new byte[] { 0xF5 }),
            };
        }
    }
}

namespace SharpAssembler.Architectures.X86
{
    partial class Instr
    {
        /// <summary>
        /// Creates a new CMC (Complement Carry Flag) instruction.
        /// </summary>
        /// <returns>The created instruction.</returns>
        public static X86Instruction Cmc()
        {
            return X86Opcode.Cmc.CreateInstruction();
        }
    }

    partial class X86Opcode
    {
        /// <summary>
        /// The CMC (Complement Carry Flag) instruction opcode.
        /// </summary>
        public static readonly X86Opcode Cmc = new CmcOpcode();
    }
}

//////////////////////////////////////////////////////
//                     WARNING                      //
//     The contents of this file is generated.      //
//    DO NOT MODIFY, your changes will be lost!     //
//////////////////////////////////////////////////////
