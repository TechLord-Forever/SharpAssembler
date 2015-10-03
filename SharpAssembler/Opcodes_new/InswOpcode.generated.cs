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
    /// The INSW (Input String) instruction opcode.
    /// </summary>
    public class InswOpcode : X86Opcode
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="InswOpcode"/> class.
        /// </summary>
        public InswOpcode()
            : base("insw", GetOpcodeVariants())
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
                // INSW
                new X86OpcodeVariant(
                    new byte[] { 0x6D }, DataSize.Bit16),
            };
        }
    }
}

namespace SharpAssembler.Architectures.X86
{
    partial class Instr
    {
        /// <summary>
        /// Creates a new INSW (Input String) instruction.
        /// </summary>
        /// <returns>The created instruction.</returns>
        public static X86Instruction Insw()
        {
            return X86Opcode.Insw.CreateInstruction();
        }
    }

    partial class X86Opcode
    {
        /// <summary>
        /// The INSW (Input String) instruction opcode.
        /// </summary>
        public static readonly X86Opcode Insw = new InswOpcode();
    }
}

//////////////////////////////////////////////////////
//                     WARNING                      //
//     The contents of this file is generated.      //
//    DO NOT MODIFY, your changes will be lost!     //
//////////////////////////////////////////////////////
