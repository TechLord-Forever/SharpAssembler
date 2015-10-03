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
    /// The CMOVL (Move if less, or not greater or equal) instruction opcode.
    /// </summary>
    public class CMovLOpcode : X86Opcode
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="CMovLOpcode"/> class.
        /// </summary>
        public CMovLOpcode()
            : base("cmovl", GetOpcodeVariants())
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
                // CMOVL reg16, reg/mem16
                new X86OpcodeVariant(
                    new byte[] { 0x0F, 0x4C },
                    new OperandDescriptor(OperandType.RegisterOperand, RegisterType.GeneralPurpose16Bit),
                    new OperandDescriptor(OperandType.RegisterOrMemoryOperand, RegisterType.GeneralPurpose16Bit)),
                // CMOVL reg32, reg/mem32
                new X86OpcodeVariant(
                    new byte[] { 0x0F, 0x4C },
                    new OperandDescriptor(OperandType.RegisterOperand, RegisterType.GeneralPurpose32Bit),
                    new OperandDescriptor(OperandType.RegisterOrMemoryOperand, RegisterType.GeneralPurpose32Bit)),
                // CMOVL reg64, reg/mem64
                new X86OpcodeVariant(
                    new byte[] { 0x0F, 0x4C },
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
        /// Creates a new CMOVL (Move if less, or not greater or equal) instruction.
        /// </summary>
        /// <param name="destination">A register.</param>
        /// <param name="source">A register or memory operand.</param>
        /// <returns>The created instruction.</returns>
        public static X86Instruction CMovL(Register destination, Register source)
        {
            return X86Opcode.CMovL.CreateInstruction(new RegisterOperand(destination), new RegisterOperand(source));
        }

        /// <summary>
        /// Creates a new CMOVL (Move if less, or not greater or equal) instruction.
        /// </summary>
        /// <param name="destination">A register.</param>
        /// <param name="source">A register or memory operand.</param>
        /// <returns>The created instruction.</returns>
        public static X86Instruction CMovL(Register destination, EffectiveAddress source)
        {
            return X86Opcode.CMovL.CreateInstruction(new RegisterOperand(destination), source);
        }



        /// <summary>
        /// Creates a new CMOVL (Move if less, or not greater or equal) instruction.
        /// </summary>
        /// <param name="destination">A register.</param>
        /// <param name="source">A register or memory operand.</param>
        /// <returns>The created instruction.</returns>
        public static X86Instruction CMovNGE(Register destination, Register source)
        {
            return X86Opcode.CMovL.CreateInstruction(new RegisterOperand(destination), new RegisterOperand(source));
        }

        /// <summary>
        /// Creates a new CMOVL (Move if less, or not greater or equal) instruction.
        /// </summary>
        /// <param name="destination">A register.</param>
        /// <param name="source">A register or memory operand.</param>
        /// <returns>The created instruction.</returns>
        public static X86Instruction CMovNGE(Register destination, EffectiveAddress source)
        {
            return X86Opcode.CMovL.CreateInstruction(new RegisterOperand(destination), source);
        }
    }

    partial class X86Opcode
    {
        /// <summary>
        /// The CMOVL (Move if less, or not greater or equal) instruction opcode.
        /// </summary>
        public static readonly X86Opcode CMovL = new CMovLOpcode();
    }
}

//////////////////////////////////////////////////////
//                     WARNING                      //
//     The contents of this file is generated.      //
//    DO NOT MODIFY, your changes will be lost!     //
//////////////////////////////////////////////////////
