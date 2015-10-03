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
    /// The AAA (ASCII Adjust After Addition) instruction opcode.
    /// </summary>
    public class AaaOpcode : X86Opcode
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="AaaOpcode"/> class.
        /// </summary>
        public AaaOpcode()
            : base("aaa", GetOpcodeVariants())
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
                // AAA
                new X86OpcodeVariant(
                    new byte[] { 0x37 })
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
        /// Creates a new AAA (ASCII Adjust After Addition) instruction.
        /// </summary>
        /// <returns>The created instruction.</returns>
        public static X86Instruction Aaa()
        {
            return X86Opcode.Aaa.CreateInstruction();
        }
    }

    partial class X86Opcode
    {
        /// <summary>
        /// The AAA (ASCII Adjust After Addition) instruction opcode.
        /// </summary>
        public static readonly X86Opcode Aaa = new AaaOpcode();
    }
}

//////////////////////////////////////////////////////
//                     WARNING                      //
//     The contents of this file is generated.      //
//    DO NOT MODIFY, your changes will be lost!     //
//////////////////////////////////////////////////////
