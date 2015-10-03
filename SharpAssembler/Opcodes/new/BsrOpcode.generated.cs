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
    /// The BSR (Bit Scan Reverse) instruction opcode.
    /// </summary>
    public class BsrOpcode : X86Opcode
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="BsrOpcode"/> class.
        /// </summary>
        public BsrOpcode()
            : base("bsr", GetOpcodeVariants())
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
                // BSR reg16, reg/mem16
                new X86OpcodeVariant(
                    new byte[] { 0x0F, 0xBD },
                    new OperandDescriptor(OperandType.RegisterOperand, RegisterType.GeneralPurpose16Bit),
                    new OperandDescriptor(OperandType.RegisterOrMemoryOperand, RegisterType.GeneralPurpose16Bit)),
                // BSR reg32, reg/mem32
                new X86OpcodeVariant(
                    new byte[] { 0x0F, 0xBD },
                    new OperandDescriptor(OperandType.RegisterOperand, RegisterType.GeneralPurpose32Bit),
                    new OperandDescriptor(OperandType.RegisterOrMemoryOperand, RegisterType.GeneralPurpose32Bit)),
                // BSR reg64, reg/mem64
                new X86OpcodeVariant(
                    new byte[] { 0x0F, 0xBD },
                    new OperandDescriptor(OperandType.RegisterOperand, RegisterType.GeneralPurpose64Bit),
                    new OperandDescriptor(OperandType.RegisterOrMemoryOperand, RegisterType.GeneralPurpose64Bit)),
            };
        }
    }
}

namespace SharpAssembler.Architectures.X86
{
    partial class Instr
    {
        /// <summary>
        /// Creates a new BSR (Bit Scan Reverse) instruction.
        /// </summary>
        /// <param name="destination">A register.</param>
        /// <param name="subject">A register or memory operand.</param>
        /// <returns>The created instruction.</returns>
        public static X86Instruction Bsr(Register destination, Register subject)
        {
            return X86Opcode.Bsr.CreateInstruction(new RegisterOperand(destination), new RegisterOperand(subject));
        }

        /// <summary>
        /// Creates a new BSR (Bit Scan Reverse) instruction.
        /// </summary>
        /// <param name="destination">A register.</param>
        /// <param name="subject">A register or memory operand.</param>
        /// <returns>The created instruction.</returns>
        public static X86Instruction Bsr(Register destination, EffectiveAddress subject)
        {
            return X86Opcode.Bsr.CreateInstruction(new RegisterOperand(destination), subject);
        }
    }

    partial class X86Opcode
    {
        /// <summary>
        /// The BSR (Bit Scan Reverse) instruction opcode.
        /// </summary>
        public static readonly X86Opcode Bsr = new BsrOpcode();
    }
}

//////////////////////////////////////////////////////
//                     WARNING                      //
//     The contents of this file is generated.      //
//    DO NOT MODIFY, your changes will be lost!     //
//////////////////////////////////////////////////////
