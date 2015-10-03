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
    /// The CWDE (Convert to Sign-Extended) instruction opcode.
    /// </summary>
    public class CwdeOpcode : X86Opcode
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="CwdeOpcode"/> class.
        /// </summary>
        public CwdeOpcode()
            : base("cwde", GetOpcodeVariants())
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
                // CWDE
                new X86OpcodeVariant(
                    new byte[] { 0x98 }, DataSize.Bit32),
            };
        }
    }
}

namespace SharpAssembler.Architectures.X86
{
    partial class Instr
    {
        /// <summary>
        /// Creates a new CWDE (Convert to Sign-Extended) instruction.
        /// </summary>
        /// <returns>The created instruction.</returns>
        public static X86Instruction Cwde()
        {
            return X86Opcode.Cwde.CreateInstruction();
        }
    }

    partial class X86Opcode
    {
        /// <summary>
        /// The CWDE (Convert to Sign-Extended) instruction opcode.
        /// </summary>
        public static readonly X86Opcode Cwde = new CwdeOpcode();
    }
}

//////////////////////////////////////////////////////
//                     WARNING                      //
//     The contents of this file is generated.      //
//    DO NOT MODIFY, your changes will be lost!     //
//////////////////////////////////////////////////////
