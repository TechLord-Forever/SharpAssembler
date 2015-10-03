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
    /// The CWD (Convert to Sign-Extended) instruction opcode.
    /// </summary>
    public class CwdOpcode : X86Opcode
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="CwdOpcode"/> class.
        /// </summary>
        public CwdOpcode()
            : base("cwd", GetOpcodeVariants())
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
                // CWD
                new X86OpcodeVariant(
                    new byte[] { 0x99 }, DataSize.Bit16),
            };
        }
    }
}

namespace SharpAssembler.Architectures.X86
{
    partial class Instr
    {
        /// <summary>
        /// Creates a new CWD (Convert to Sign-Extended) instruction.
        /// </summary>
        /// <returns>The created instruction.</returns>
        public static X86Instruction Cwd()
        {
            return X86Opcode.Cwd.CreateInstruction();
        }
    }

    partial class X86Opcode
    {
        /// <summary>
        /// The CWD (Convert to Sign-Extended) instruction opcode.
        /// </summary>
        public static readonly X86Opcode Cwd = new CwdOpcode();
    }
}

//////////////////////////////////////////////////////
//                     WARNING                      //
//     The contents of this file is generated.      //
//    DO NOT MODIFY, your changes will be lost!     //
//////////////////////////////////////////////////////
