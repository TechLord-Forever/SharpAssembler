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
    /// The CMOVNP (Move if not parity, or parity odd) instruction opcode.
    /// </summary>
    public class CMovNPOpcode : X86Opcode
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="CMovNPOpcode"/> class.
        /// </summary>
        public CMovNPOpcode()
            : base("cmovnp", GetOpcodeVariants())
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
                // CMOVNP reg16, reg/mem16
                new X86OpcodeVariant(
                    new byte[] { 0x0F, 0x4B },
                    new OperandDescriptor(OperandType.RegisterOperand, RegisterType.GeneralPurpose16Bit),
                    new OperandDescriptor(OperandType.RegisterOrMemoryOperand, RegisterType.GeneralPurpose16Bit)),
                // CMOVNP reg32, reg/mem32
                new X86OpcodeVariant(
                    new byte[] { 0x0F, 0x4B },
                    new OperandDescriptor(OperandType.RegisterOperand, RegisterType.GeneralPurpose32Bit),
                    new OperandDescriptor(OperandType.RegisterOrMemoryOperand, RegisterType.GeneralPurpose32Bit)),
                // CMOVNP reg64, reg/mem64
                new X86OpcodeVariant(
                    new byte[] { 0x0F, 0x4B },
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
        /// Creates a new CMOVNP (Move if not parity, or parity odd) instruction.
        /// </summary>
        /// <param name="destination">A register.</param>
        /// <param name="source">A register or memory operand.</param>
        /// <returns>The created instruction.</returns>
        public static X86Instruction CMovNP(Register destination, Register source)
        {
            return X86Opcode.CMovNP.CreateInstruction(new RegisterOperand(destination), new RegisterOperand(source));
        }

        /// <summary>
        /// Creates a new CMOVNP (Move if not parity, or parity odd) instruction.
        /// </summary>
        /// <param name="destination">A register.</param>
        /// <param name="source">A register or memory operand.</param>
        /// <returns>The created instruction.</returns>
        public static X86Instruction CMovNP(Register destination, EffectiveAddress source)
        {
            return X86Opcode.CMovNP.CreateInstruction(new RegisterOperand(destination), source);
        }



        /// <summary>
        /// Creates a new CMOVNP (Move if not parity, or parity odd) instruction.
        /// </summary>
        /// <param name="destination">A register.</param>
        /// <param name="source">A register or memory operand.</param>
        /// <returns>The created instruction.</returns>
        public static X86Instruction CMovPO(Register destination, Register source)
        {
            return X86Opcode.CMovNP.CreateInstruction(new RegisterOperand(destination), new RegisterOperand(source));
        }

        /// <summary>
        /// Creates a new CMOVNP (Move if not parity, or parity odd) instruction.
        /// </summary>
        /// <param name="destination">A register.</param>
        /// <param name="source">A register or memory operand.</param>
        /// <returns>The created instruction.</returns>
        public static X86Instruction CMovPO(Register destination, EffectiveAddress source)
        {
            return X86Opcode.CMovNP.CreateInstruction(new RegisterOperand(destination), source);
        }
    }

    partial class X86Opcode
    {
        /// <summary>
        /// The CMOVNP (Move if not parity, or parity odd) instruction opcode.
        /// </summary>
        public static readonly X86Opcode CMovNP = new CMovNPOpcode();
    }
}

//////////////////////////////////////////////////////
//                     WARNING                      //
//     The contents of this file is generated.      //
//    DO NOT MODIFY, your changes will be lost!     //
//////////////////////////////////////////////////////
