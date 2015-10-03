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
    /// The BTR (Bit Test and Reset) instruction opcode.
    /// </summary>
    public class BtrOpcode : X86Opcode
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="BtrOpcode"/> class.
        /// </summary>
        public BtrOpcode()
            : base("btr", GetOpcodeVariants())
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
                // BTR reg/mem16, imm8
                new X86OpcodeVariant(
                    new byte[] { 0x0F, 0xBA }, 6,
                    new OperandDescriptor(OperandType.RegisterOrMemoryOperand, RegisterType.GeneralPurpose16Bit),
                    new OperandDescriptor(OperandType.Immediate, DataSize.Bit8)),
                // BTR reg/mem32, imm8
                new X86OpcodeVariant(
                    new byte[] { 0x0F, 0xBA }, 6,
                    new OperandDescriptor(OperandType.RegisterOrMemoryOperand, RegisterType.GeneralPurpose32Bit),
                    new OperandDescriptor(OperandType.Immediate, DataSize.Bit8)),
                // BTR reg/mem64, imm8
                new X86OpcodeVariant(
                    new byte[] { 0x0F, 0xBA }, 6,
                    new OperandDescriptor(OperandType.RegisterOrMemoryOperand, RegisterType.GeneralPurpose64Bit),
                    new OperandDescriptor(OperandType.Immediate, DataSize.Bit8)),
                // BTR reg/mem16, reg16
                new X86OpcodeVariant(
                    new byte[] { 0x0F, 0xB3 },
                    new OperandDescriptor(OperandType.RegisterOrMemoryOperand, RegisterType.GeneralPurpose16Bit),
                    new OperandDescriptor(OperandType.RegisterOperand, RegisterType.GeneralPurpose16Bit)),
                // BTR reg/mem32, reg32
                new X86OpcodeVariant(
                    new byte[] { 0x0F, 0xB3 },
                    new OperandDescriptor(OperandType.RegisterOrMemoryOperand, RegisterType.GeneralPurpose32Bit),
                    new OperandDescriptor(OperandType.RegisterOperand, RegisterType.GeneralPurpose32Bit)),
                // BTR reg/mem64, reg64
                new X86OpcodeVariant(
                    new byte[] { 0x0F, 0xB3 },
                    new OperandDescriptor(OperandType.RegisterOrMemoryOperand, RegisterType.GeneralPurpose64Bit),
                    new OperandDescriptor(OperandType.RegisterOperand, RegisterType.GeneralPurpose64Bit)),
            };
        }
    }
}

namespace SharpAssembler.Architectures.X86
{
    partial class Instr
    {
        /// <summary>
        /// Creates a new BTR (Bit Test and Reset) instruction.
        /// </summary>
        /// <param name="value">A register or memory operand.</param>
        /// <param name="bitindex">An immediate value.</param>
        /// <returns>The created instruction.</returns>
        public static X86Instruction Btr(Register value, byte bitindex)
        {
            return X86Opcode.Btr.CreateInstruction(new RegisterOperand(value), new Immediate(bitindex, DataSize.Bit8));
        }

        /// <summary>
        /// Creates a new BTR (Bit Test and Reset) instruction.
        /// </summary>
        /// <param name="value">A register or memory operand.</param>
        /// <param name="bitindex">An immediate value.</param>
        /// <returns>The created instruction.</returns>
        [CLSCompliant(false)]
        public static X86Instruction Btr(Register value, sbyte bitindex)
        {
            return X86Opcode.Btr.CreateInstruction(new RegisterOperand(value), new Immediate(bitindex, DataSize.Bit8));
        }

        /// <summary>
        /// Creates a new BTR (Bit Test and Reset) instruction.
        /// </summary>
        /// <param name="value">A register or memory operand.</param>
        /// <param name="bitindex">An immediate value.</param>
        /// <returns>The created instruction.</returns>
        public static X86Instruction Btr(EffectiveAddress value, byte bitindex)
        {
            return X86Opcode.Btr.CreateInstruction(value, new Immediate(bitindex, DataSize.Bit8));
        }

        /// <summary>
        /// Creates a new BTR (Bit Test and Reset) instruction.
        /// </summary>
        /// <param name="value">A register or memory operand.</param>
        /// <param name="bitindex">An immediate value.</param>
        /// <returns>The created instruction.</returns>
        [CLSCompliant(false)]
        public static X86Instruction Btr(EffectiveAddress value, sbyte bitindex)
        {
            return X86Opcode.Btr.CreateInstruction(value, new Immediate(bitindex, DataSize.Bit8));
        }

        /// <summary>
        /// Creates a new BTR (Bit Test and Reset) instruction.
        /// </summary>
        /// <param name="value">A register or memory operand.</param>
        /// <param name="bitindex">A register.</param>
        /// <returns>The created instruction.</returns>
        public static X86Instruction Btr(Register value, Register bitindex)
        {
            return X86Opcode.Btr.CreateInstruction(new RegisterOperand(value), new RegisterOperand(bitindex));
        }

        /// <summary>
        /// Creates a new BTR (Bit Test and Reset) instruction.
        /// </summary>
        /// <param name="value">A register or memory operand.</param>
        /// <param name="bitindex">A register.</param>
        /// <returns>The created instruction.</returns>
        public static X86Instruction Btr(EffectiveAddress value, Register bitindex)
        {
            return X86Opcode.Btr.CreateInstruction(value, new RegisterOperand(bitindex));
        }
    }

    partial class X86Opcode
    {
        /// <summary>
        /// The BTR (Bit Test and Reset) instruction opcode.
        /// </summary>
        public static readonly X86Opcode Btr = new BtrOpcode();
    }
}

//////////////////////////////////////////////////////
//                     WARNING                      //
//     The contents of this file is generated.      //
//    DO NOT MODIFY, your changes will be lost!     //
//////////////////////////////////////////////////////
