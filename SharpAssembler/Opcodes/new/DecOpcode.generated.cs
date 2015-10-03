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
    /// The DEC (Decrement by 1) instruction opcode.
    /// </summary>
    public class DecOpcode : X86Opcode
    {
        /// <inheritdoc />
        public override bool CanLock
        {
            get { return true; }
        }

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="DecOpcode"/> class.
        /// </summary>
        public DecOpcode()
            : base("dec", GetOpcodeVariants())
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
                // DEC reg16
                new X86OpcodeVariant(
                    new byte[] { 0x48 },
                    new OperandDescriptor(OperandType.RegisterOperand, RegisterType.GeneralPurpose16Bit, OperandEncoding.OpcodeAdd))
                    { SupportedModes = ProcessorModes.ProtectedReal },
                // DEC reg32
                new X86OpcodeVariant(
                    new byte[] { 0x48 },
                    new OperandDescriptor(OperandType.RegisterOperand, RegisterType.GeneralPurpose32Bit, OperandEncoding.OpcodeAdd))
                    { SupportedModes = ProcessorModes.ProtectedReal },
                // DEC reg/mem8
                new X86OpcodeVariant(
                    new byte[] { 0xFE }, 1,
                    new OperandDescriptor(OperandType.RegisterOrMemoryOperand, RegisterType.GeneralPurpose8Bit)),
                // DEC reg/mem16
                new X86OpcodeVariant(
                    new byte[] { 0xFF }, 1,
                    new OperandDescriptor(OperandType.RegisterOrMemoryOperand, RegisterType.GeneralPurpose16Bit)),
                // DEC reg/mem32
                new X86OpcodeVariant(
                    new byte[] { 0xFF }, 1,
                    new OperandDescriptor(OperandType.RegisterOrMemoryOperand, RegisterType.GeneralPurpose32Bit)),
                // DEC reg/mem64
                new X86OpcodeVariant(
                    new byte[] { 0xFF }, 1,
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
        /// Creates a new DEC (Decrement by 1) instruction.
        /// </summary>
        /// <param name="destination">A register.</param>
        /// <returns>The created instruction.</returns>
        public static X86Instruction Dec(Register destination)
        {
            return X86Opcode.Dec.CreateInstruction(new RegisterOperand(destination));
        }

        /// <summary>
        /// Creates a new DEC (Decrement by 1) instruction.
        /// </summary>
        /// <param name="destination">A register or memory operand.</param>
        /// <returns>The created instruction.</returns>
        public static X86Instruction Dec(EffectiveAddress destination)
        {
            return X86Opcode.Dec.CreateInstruction(destination);
        }
    }

    partial class X86Opcode
    {
        /// <summary>
        /// The DEC (Decrement by 1) instruction opcode.
        /// </summary>
        public static readonly X86Opcode Dec = new DecOpcode();
    }
}

//////////////////////////////////////////////////////
//                     WARNING                      //
//     The contents of this file is generated.      //
//    DO NOT MODIFY, your changes will be lost!     //
//////////////////////////////////////////////////////
