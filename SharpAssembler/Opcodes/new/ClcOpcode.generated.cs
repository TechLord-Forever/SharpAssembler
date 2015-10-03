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
    /// The CLC (Clear Carry Flag) instruction opcode.
    /// </summary>
    public class ClcOpcode : X86Opcode
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ClcOpcode"/> class.
        /// </summary>
        public ClcOpcode()
            : base("clc", GetOpcodeVariants())
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
                // CLC
                new X86OpcodeVariant(
                    new byte[] { 0xF8 }),
            };
        }
    }
}

namespace SharpAssembler.Architectures.X86
{
    partial class Instr
    {
        /// <summary>
        /// Creates a new CLC (Clear Carry Flag) instruction.
        /// </summary>
        /// <returns>The created instruction.</returns>
        public static X86Instruction Clc()
        {
            return X86Opcode.Clc.CreateInstruction();
        }
    }

    partial class X86Opcode
    {
        /// <summary>
        /// The CLC (Clear Carry Flag) instruction opcode.
        /// </summary>
        public static readonly X86Opcode Clc = new ClcOpcode();
    }
}

//////////////////////////////////////////////////////
//                     WARNING                      //
//     The contents of this file is generated.      //
//    DO NOT MODIFY, your changes will be lost!     //
//////////////////////////////////////////////////////
