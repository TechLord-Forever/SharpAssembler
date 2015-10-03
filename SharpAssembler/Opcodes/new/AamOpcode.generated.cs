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
    /// The AAM (ASCII Adjust After Multiply) instruction opcode.
    /// </summary>
    public class AamOpcode : X86Opcode
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="AamOpcode"/> class.
        /// </summary>
        public AamOpcode()
            : base("aam", GetOpcodeVariants())
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
                // AAM imm8
                new X86OpcodeVariant(
                    new byte[] { 0xD4 },
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
        /// Creates a new AAM (ASCII Adjust After Multiply) instruction.
        /// </summary>
        /// <param name="base">An immediate value.</param>
        /// <returns>The created instruction.</returns>
        public static X86Instruction Aam(byte @base)
        {
            return X86Opcode.Aam.CreateInstruction(new Immediate(@base, DataSize.Bit8));
        }

        /// <summary>
        /// Creates a new AAM (ASCII Adjust After Multiply) instruction.
        /// </summary>
        /// <param name="base">An immediate value.</param>
        /// <returns>The created instruction.</returns>
        [CLSCompliant(false)]
        public static X86Instruction Aam(sbyte @base)
        {
            return X86Opcode.Aam.CreateInstruction(new Immediate(@base, DataSize.Bit8));
        }
    }

    partial class X86Opcode
    {
        /// <summary>
        /// The AAM (ASCII Adjust After Multiply) instruction opcode.
        /// </summary>
        public static readonly X86Opcode Aam = new AamOpcode();
    }
}

//////////////////////////////////////////////////////
//                     WARNING                      //
//     The contents of this file is generated.      //
//    DO NOT MODIFY, your changes will be lost!     //
//////////////////////////////////////////////////////
