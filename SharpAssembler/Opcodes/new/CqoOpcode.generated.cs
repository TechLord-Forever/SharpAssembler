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
    /// The CQO (Convert to Sign-Extended) instruction opcode.
    /// </summary>
    public class CqoOpcode : X86Opcode
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="CqoOpcode"/> class.
        /// </summary>
        public CqoOpcode()
            : base("cqo", GetOpcodeVariants())
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
                // CQO
                new X86OpcodeVariant(
                    new byte[] { 0x99 }, DataSize.Bit64),
            };
        }
    }
}

namespace SharpAssembler.Architectures.X86
{
    partial class Instr
    {
        /// <summary>
        /// Creates a new CQO (Convert to Sign-Extended) instruction.
        /// </summary>
        /// <returns>The created instruction.</returns>
        public static X86Instruction Cqo()
        {
            return X86Opcode.Cqo.CreateInstruction();
        }
    }

    partial class X86Opcode
    {
        /// <summary>
        /// The CQO (Convert to Sign-Extended) instruction opcode.
        /// </summary>
        public static readonly X86Opcode Cqo = new CqoOpcode();
    }
}

//////////////////////////////////////////////////////
//                     WARNING                      //
//     The contents of this file is generated.      //
//    DO NOT MODIFY, your changes will be lost!     //
//////////////////////////////////////////////////////
