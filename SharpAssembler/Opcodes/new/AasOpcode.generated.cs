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
    /// The AAS (ASCII Adjust After Subtraction) instruction opcode.
    /// </summary>
    public class AasOpcode : X86Opcode
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="AasOpcode"/> class.
        /// </summary>
        public AasOpcode()
            : base("aas", GetOpcodeVariants())
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
                // AAS
                new X86OpcodeVariant(
                    new byte[] { 0x3F })
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
        /// Creates a new AAS (ASCII Adjust After Subtraction) instruction.
        /// </summary>
        /// <returns>The created instruction.</returns>
        public static X86Instruction Aas()
        {
            return X86Opcode.Aas.CreateInstruction();
        }
    }

    partial class X86Opcode
    {
        /// <summary>
        /// The AAS (ASCII Adjust After Subtraction) instruction opcode.
        /// </summary>
        public static readonly X86Opcode Aas = new AasOpcode();
    }
}

//////////////////////////////////////////////////////
//                     WARNING                      //
//     The contents of this file is generated.      //
//    DO NOT MODIFY, your changes will be lost!     //
//////////////////////////////////////////////////////
