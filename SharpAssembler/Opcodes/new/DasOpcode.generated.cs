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
    /// The DAS (Decimal Adjust after Subtraction) instruction opcode.
    /// </summary>
    public class DasOpcode : X86Opcode
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="DasOpcode"/> class.
        /// </summary>
        public DasOpcode()
            : base("das", GetOpcodeVariants())
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
                // DAS
                new X86OpcodeVariant(
                    new byte[] { 0x2F })
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
        /// Creates a new DAS (Decimal Adjust after Subtraction) instruction.
        /// </summary>
        /// <returns>The created instruction.</returns>
        public static X86Instruction Das()
        {
            return X86Opcode.Das.CreateInstruction();
        }
    }

    partial class X86Opcode
    {
        /// <summary>
        /// The DAS (Decimal Adjust after Subtraction) instruction opcode.
        /// </summary>
        public static readonly X86Opcode Das = new DasOpcode();
    }
}

//////////////////////////////////////////////////////
//                     WARNING                      //
//     The contents of this file is generated.      //
//    DO NOT MODIFY, your changes will be lost!     //
//////////////////////////////////////////////////////
