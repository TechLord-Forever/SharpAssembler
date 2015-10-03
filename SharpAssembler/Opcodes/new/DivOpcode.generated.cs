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
    /// The DIV (Unsigned Divide) instruction opcode.
    /// </summary>
    public class DivOpcode : X86Opcode
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="DivOpcode"/> class.
        /// </summary>
        public DivOpcode()
            : base("div", GetOpcodeVariants())
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
                // DIV reg/mem8
                new X86OpcodeVariant(
                    new byte[] { 0xF6 }, 6,
                    new OperandDescriptor(OperandType.RegisterOrMemoryOperand, RegisterType.GeneralPurpose8Bit)),
                // DIV reg/mem16
                new X86OpcodeVariant(
                    new byte[] { 0xF7 }, 6,
                    new OperandDescriptor(OperandType.RegisterOrMemoryOperand, RegisterType.GeneralPurpose16Bit)),
                // DIV reg/mem32
                new X86OpcodeVariant(
                    new byte[] { 0xF7 }, 6,
                    new OperandDescriptor(OperandType.RegisterOrMemoryOperand, RegisterType.GeneralPurpose32Bit)),
                // DIV reg/mem64
                new X86OpcodeVariant(
                    new byte[] { 0xF7 }, 6,
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
        /// Creates a new DIV (Unsigned Divide) instruction.
        /// </summary>
        /// <param name="source">A register or memory operand.</param>
        /// <returns>The created instruction.</returns>
        public static X86Instruction Div(Register source)
        {
            return X86Opcode.Div.CreateInstruction(new RegisterOperand(source));
        }

        /// <summary>
        /// Creates a new DIV (Unsigned Divide) instruction.
        /// </summary>
        /// <param name="source">A register or memory operand.</param>
        /// <returns>The created instruction.</returns>
        public static X86Instruction Div(EffectiveAddress source)
        {
            return X86Opcode.Div.CreateInstruction(source);
        }
    }

    partial class X86Opcode
    {
        /// <summary>
        /// The DIV (Unsigned Divide) instruction opcode.
        /// </summary>
        public static readonly X86Opcode Div = new DivOpcode();
    }
}

//////////////////////////////////////////////////////
//                     WARNING                      //
//     The contents of this file is generated.      //
//    DO NOT MODIFY, your changes will be lost!     //
//////////////////////////////////////////////////////
