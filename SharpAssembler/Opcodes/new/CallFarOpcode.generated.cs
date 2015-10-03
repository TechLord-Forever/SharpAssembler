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
    /// The CALL FAR (Far Procedure Call) instruction opcode.
    /// </summary>
    public class CallFarOpcode : X86Opcode
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="CallFarOpcode"/> class.
        /// </summary>
        public CallFarOpcode()
            : base("call far", GetOpcodeVariants())
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
                // CALL FAR pntr16:16
                new X86OpcodeVariant(
                    new byte[] { 0x9A },
                    new OperandDescriptor(OperandType.FarPointer, DataSize.Bit16))
                    { SupportedModes = ProcessorModes.ProtectedReal },
                // CALL FAR pntr16:32
                new X86OpcodeVariant(
                    new byte[] { 0x9A },
                    new OperandDescriptor(OperandType.FarPointer, DataSize.Bit32))
                    { SupportedModes = ProcessorModes.ProtectedReal },
                // CALL FAR mem16
                new X86OpcodeVariant(
                    new byte[] { 0xFF }, 3,
                    new OperandDescriptor(OperandType.MemoryOperand, DataSize.Bit16)),
                // CALL FAR mem32
                new X86OpcodeVariant(
                    new byte[] { 0xFF }, 3,
                    new OperandDescriptor(OperandType.MemoryOperand, DataSize.Bit32)),
            };
        }
    }
}

namespace SharpAssembler.Architectures.X86
{
    partial class Instr
    {
        /// <summary>
        /// Creates a new CALL FAR (Far Procedure Call) instruction.
        /// </summary>
        /// <param name="target">A far pointer.</param>
        /// <returns>The created instruction.</returns>
        public static X86Instruction CallFar(FarPointer target)
        {
            return X86Opcode.CallFar.CreateInstruction(target);
        }

        /// <summary>
        /// Creates a new CALL FAR (Far Procedure Call) instruction.
        /// </summary>
        /// <param name="target">A far pointer.</param>
        /// <returns>The created instruction.</returns>
        public static X86Instruction CallFar(EffectiveAddress target)
        {
            return X86Opcode.CallFar.CreateInstruction(target);
        }
    }

    partial class X86Opcode
    {
        /// <summary>
        /// The CALL FAR (Far Procedure Call) instruction opcode.
        /// </summary>
        public static readonly X86Opcode CallFar = new CallFarOpcode();
    }
}

//////////////////////////////////////////////////////
//                     WARNING                      //
//     The contents of this file is generated.      //
//    DO NOT MODIFY, your changes will be lost!     //
//////////////////////////////////////////////////////
