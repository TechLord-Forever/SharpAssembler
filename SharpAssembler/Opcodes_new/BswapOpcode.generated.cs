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
    /// The BSWAP (Byte Swap) instruction opcode.
    /// </summary>
    public class BswapOpcode : X86Opcode
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="BswapOpcode"/> class.
        /// </summary>
        public BswapOpcode()
            : base("bswap", GetOpcodeVariants())
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
                // BSWAP reg32
                new X86OpcodeVariant(
                    new byte[] { 0x0F, 0xC8 },
                    new OperandDescriptor(OperandType.RegisterOperand, RegisterType.GeneralPurpose32Bit, OperandEncoding.OpcodeAdd)),
                // BSWAP reg64
                new X86OpcodeVariant(
                    new byte[] { 0x0F, 0xC8 },
                    new OperandDescriptor(OperandType.RegisterOperand, RegisterType.GeneralPurpose64Bit, OperandEncoding.OpcodeAdd)),
            };
        }
    }
}

namespace SharpAssembler.Architectures.X86
{
    partial class Instr
    {
        /// <summary>
        /// Creates a new BSWAP (Byte Swap) instruction.
        /// </summary>
        /// <param name="value">A register.</param>
        /// <returns>The created instruction.</returns>
        public static X86Instruction Bswap(Register value)
        {
            return X86Opcode.Bswap.CreateInstruction(new RegisterOperand(value));
        }
    }

    partial class X86Opcode
    {
        /// <summary>
        /// The BSWAP (Byte Swap) instruction opcode.
        /// </summary>
        public static readonly X86Opcode Bswap = new BswapOpcode();
    }
}

//////////////////////////////////////////////////////
//                     WARNING                      //
//     The contents of this file is generated.      //
//    DO NOT MODIFY, your changes will be lost!     //
//////////////////////////////////////////////////////
