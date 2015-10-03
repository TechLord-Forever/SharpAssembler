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
    /// The AAD (ASCII Adjust Before Division) instruction opcode.
    /// </summary>
    public class AadOpcode : X86Opcode
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="AadOpcode"/> class.
        /// </summary>
        public AadOpcode()
            : base("aad", GetOpcodeVariants())
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
                // AAD imm8
                new X86OpcodeVariant(
                    new byte[] { 0xD5 },
                    new OperandDescriptor(OperandType.Immediate, DataSize.Bit8))
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
        /// Creates a new AAD (ASCII Adjust Before Division) instruction.
        /// </summary>
        /// <param name="base">An immediate value.</param>
        /// <returns>The created instruction.</returns>
        public static X86Instruction Aad(byte @base)
        {
            return X86Opcode.Aad.CreateInstruction(new Immediate(@base, DataSize.Bit8));
        }

        /// <summary>
        /// Creates a new AAD (ASCII Adjust Before Division) instruction.
        /// </summary>
        /// <param name="base">An immediate value.</param>
        /// <returns>The created instruction.</returns>
        [CLSCompliant(false)]
        public static X86Instruction Aad(sbyte @base)
        {
            return X86Opcode.Aad.CreateInstruction(new Immediate(@base, DataSize.Bit8));
        }
    }

    partial class X86Opcode
    {
        /// <summary>
        /// The AAD (ASCII Adjust Before Division) instruction opcode.
        /// </summary>
        public static readonly X86Opcode Aad = new AadOpcode();
    }
}

//////////////////////////////////////////////////////
//                     WARNING                      //
//     The contents of this file is generated.      //
//    DO NOT MODIFY, your changes will be lost!     //
//////////////////////////////////////////////////////
