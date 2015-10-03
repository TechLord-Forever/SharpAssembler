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
    /// The DAA (Decimal Adjust after Addition) instruction opcode.
    /// </summary>
    public class DaaOpcode : X86Opcode
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="DaaOpcode"/> class.
        /// </summary>
        public DaaOpcode()
            : base("daa", GetOpcodeVariants())
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
                // DAA
                new X86OpcodeVariant(
                    new byte[] { 0x27 })
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
        /// Creates a new DAA (Decimal Adjust after Addition) instruction.
        /// </summary>
        /// <returns>The created instruction.</returns>
        public static X86Instruction Daa()
        {
            return X86Opcode.Daa.CreateInstruction();
        }
    }

    partial class X86Opcode
    {
        /// <summary>
        /// The DAA (Decimal Adjust after Addition) instruction opcode.
        /// </summary>
        public static readonly X86Opcode Daa = new DaaOpcode();
    }
}

//////////////////////////////////////////////////////
//                     WARNING                      //
//     The contents of this file is generated.      //
//    DO NOT MODIFY, your changes will be lost!     //
//////////////////////////////////////////////////////
