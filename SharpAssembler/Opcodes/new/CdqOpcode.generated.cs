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
    /// The CDQ (Convert to Sign-Extended) instruction opcode.
    /// </summary>
    public class CdqOpcode : X86Opcode
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="CdqOpcode"/> class.
        /// </summary>
        public CdqOpcode()
            : base("cdq", GetOpcodeVariants())
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
                // CDQ
                new X86OpcodeVariant(
                    new byte[] { 0x99 }, DataSize.Bit32),
            };
        }
    }
}

namespace SharpAssembler.Architectures.X86
{
    partial class Instr
    {
        /// <summary>
        /// Creates a new CDQ (Convert to Sign-Extended) instruction.
        /// </summary>
        /// <returns>The created instruction.</returns>
        public static X86Instruction Cdq()
        {
            return X86Opcode.Cdq.CreateInstruction();
        }
    }

    partial class X86Opcode
    {
        /// <summary>
        /// The CDQ (Convert to Sign-Extended) instruction opcode.
        /// </summary>
        public static readonly X86Opcode Cdq = new CdqOpcode();
    }
}

//////////////////////////////////////////////////////
//                     WARNING                      //
//     The contents of this file is generated.      //
//    DO NOT MODIFY, your changes will be lost!     //
//////////////////////////////////////////////////////
