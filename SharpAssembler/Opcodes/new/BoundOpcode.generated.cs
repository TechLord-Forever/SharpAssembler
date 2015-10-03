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
    /// The BOUND (Check Array Bound) instruction opcode.
    /// </summary>
    public class BoundOpcode : X86Opcode
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="BoundOpcode"/> class.
        /// </summary>
        public BoundOpcode()
            : base("bound", GetOpcodeVariants())
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
                // BOUND reg16, mem16
                new X86OpcodeVariant(
                    new byte[] { 0x62 },
                    new OperandDescriptor(OperandType.RegisterOperand, RegisterType.GeneralPurpose16Bit),
                    new OperandDescriptor(OperandType.MemoryOperand, DataSize.Bit16))
                    { SupportedModes = ProcessorModes.ProtectedReal },
                // BOUND reg32, mem32
                new X86OpcodeVariant(
                    new byte[] { 0x62 },
                    new OperandDescriptor(OperandType.RegisterOperand, RegisterType.GeneralPurpose32Bit),
                    new OperandDescriptor(OperandType.MemoryOperand, DataSize.Bit32))
                    { SupportedModes = ProcessorModes.ProtectedReal },
            };
        }
    }
}

namespace SharpAssembler.Architectures.X86
{
    partial class Instr
    {
        /// <summary>
        /// Creates a new BOUND (Check Array Bound) instruction.
        /// </summary>
        /// <param name="index">A register.</param>
        /// <param name="bounds">A memory operand.</param>
        /// <returns>The created instruction.</returns>
        public static X86Instruction Bound(Register index, EffectiveAddress bounds)
        {
            return X86Opcode.Bound.CreateInstruction(new RegisterOperand(index), bounds);
        }
    }

    partial class X86Opcode
    {
        /// <summary>
        /// The BOUND (Check Array Bound) instruction opcode.
        /// </summary>
        public static readonly X86Opcode Bound = new BoundOpcode();
    }
}

//////////////////////////////////////////////////////
//                     WARNING                      //
//     The contents of this file is generated.      //
//    DO NOT MODIFY, your changes will be lost!     //
//////////////////////////////////////////////////////
