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
    /// The CMPXCHG (Compare and Exchange) instruction opcode.
    /// </summary>
    public class CmpXchgOpcode : X86Opcode
    {
        /// <inheritdoc />
        public override bool CanLock
        {
            get { return true; }
        }

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="CmpXchgOpcode"/> class.
        /// </summary>
        public CmpXchgOpcode()
            : base("cmpxchg", GetOpcodeVariants())
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
                // CMPXCHG reg/mem8, reg8
                new X86OpcodeVariant(
                    new byte[] { 0x0F, 0xB0 },
                    new OperandDescriptor(OperandType.RegisterOrMemoryOperand, RegisterType.GeneralPurpose8Bit),
                    new OperandDescriptor(OperandType.RegisterOperand, RegisterType.GeneralPurpose8Bit)),
                // CMPXCHG reg/mem16, reg16
                new X86OpcodeVariant(
                    new byte[] { 0x0F, 0xB1 },
                    new OperandDescriptor(OperandType.RegisterOrMemoryOperand, RegisterType.GeneralPurpose16Bit),
                    new OperandDescriptor(OperandType.RegisterOperand, RegisterType.GeneralPurpose16Bit)),
                // CMPXCHG reg/mem32, reg32
                new X86OpcodeVariant(
                    new byte[] { 0x0F, 0xB1 },
                    new OperandDescriptor(OperandType.RegisterOrMemoryOperand, RegisterType.GeneralPurpose32Bit),
                    new OperandDescriptor(OperandType.RegisterOperand, RegisterType.GeneralPurpose32Bit)),
                // CMPXCHG reg/mem64, reg64
                new X86OpcodeVariant(
                    new byte[] { 0x0F, 0xB1 },
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
        /// Creates a new CMPXCHG (Compare and Exchange) instruction.
        /// </summary>
        /// <param name="destination">A register or memory operand.</param>
        /// <param name="source">A register.</param>
        /// <returns>The created instruction.</returns>
        public static X86Instruction CmpXchg(Register destination, Register source)
        {
            return X86Opcode.CmpXchg.CreateInstruction(new RegisterOperand(destination), new RegisterOperand(source));
        }

        /// <summary>
        /// Creates a new CMPXCHG (Compare and Exchange) instruction.
        /// </summary>
        /// <param name="destination">A register or memory operand.</param>
        /// <param name="source">A register.</param>
        /// <returns>The created instruction.</returns>
        public static X86Instruction CmpXchg(EffectiveAddress destination, Register source)
        {
            return X86Opcode.CmpXchg.CreateInstruction(destination, new RegisterOperand(source));
        }
    }

    partial class X86Opcode
    {
        /// <summary>
        /// The CMPXCHG (Compare and Exchange) instruction opcode.
        /// </summary>
        public static readonly X86Opcode CmpXchg = new CmpXchgOpcode();
    }
}

//////////////////////////////////////////////////////
//                     WARNING                      //
//     The contents of this file is generated.      //
//    DO NOT MODIFY, your changes will be lost!     //
//////////////////////////////////////////////////////
