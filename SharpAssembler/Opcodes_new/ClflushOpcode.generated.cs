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
    /// The CLFLUSH (Cache Line Flush) instruction opcode.
    /// </summary>
    public class ClflushOpcode : X86Opcode
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ClflushOpcode"/> class.
        /// </summary>
        public ClflushOpcode()
            : base("clflush", GetOpcodeVariants())
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
                // CLFLUSH mem8
                new X86OpcodeVariant(
                    new byte[] { 0x0F, 0xAE }, 7,
                    new OperandDescriptor(OperandType.MemoryOperand, DataSize.Bit8)),
            };
        }
    }
}

namespace SharpAssembler.Architectures.X86
{
    partial class Instr
    {
        /// <summary>
        /// Creates a new CLFLUSH (Cache Line Flush) instruction.
        /// </summary>
        /// <param name="address">A memory operand.</param>
        /// <returns>The created instruction.</returns>
        public static X86Instruction Clflush(EffectiveAddress address)
        {
            return X86Opcode.Clflush.CreateInstruction(address);
        }
    }

    partial class X86Opcode
    {
        /// <summary>
        /// The CLFLUSH (Cache Line Flush) instruction opcode.
        /// </summary>
        public static readonly X86Opcode Clflush = new ClflushOpcode();
    }
}

//////////////////////////////////////////////////////
//                     WARNING                      //
//     The contents of this file is generated.      //
//    DO NOT MODIFY, your changes will be lost!     //
//////////////////////////////////////////////////////
