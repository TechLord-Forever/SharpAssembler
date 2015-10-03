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
    /// The IDIV (Signed Divide) instruction opcode.
    /// </summary>
    public class IdivOpcode : X86Opcode
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="IdivOpcode"/> class.
        /// </summary>
        public IdivOpcode()
            : base("idiv", GetOpcodeVariants())
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
                // IDIV reg/mem8
                new X86OpcodeVariant(
                    new byte[] { 0xF6 }, 7,
                    new OperandDescriptor(OperandType.RegisterOrMemoryOperand, RegisterType.GeneralPurpose8Bit)),
                // IDIV reg/mem16
                new X86OpcodeVariant(
                    new byte[] { 0xF7 }, 7,
                    new OperandDescriptor(OperandType.RegisterOrMemoryOperand, RegisterType.GeneralPurpose16Bit)),
                // IDIV reg/mem32
                new X86OpcodeVariant(
                    new byte[] { 0xF7 }, 7,
                    new OperandDescriptor(OperandType.RegisterOrMemoryOperand, RegisterType.GeneralPurpose32Bit)),
                // IDIV reg/mem64
                new X86OpcodeVariant(
                    new byte[] { 0xF7 }, 7,
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
        /// Creates a new IDIV (Signed Divide) instruction.
        /// </summary>
        /// <param name="source">A register or memory operand.</param>
        /// <returns>The created instruction.</returns>
        public static X86Instruction Idiv(Register source)
        {
            return X86Opcode.Idiv.CreateInstruction(new RegisterOperand(source));
        }

        /// <summary>
        /// Creates a new IDIV (Signed Divide) instruction.
        /// </summary>
        /// <param name="source">A register or memory operand.</param>
        /// <returns>The created instruction.</returns>
        public static X86Instruction Idiv(EffectiveAddress source)
        {
            return X86Opcode.Idiv.CreateInstruction(source);
        }
    }

    partial class X86Opcode
    {
        /// <summary>
        /// The IDIV (Signed Divide) instruction opcode.
        /// </summary>
        public static readonly X86Opcode Idiv = new IdivOpcode();
    }
}

//////////////////////////////////////////////////////
//                     WARNING                      //
//     The contents of this file is generated.      //
//    DO NOT MODIFY, your changes will be lost!     //
//////////////////////////////////////////////////////
