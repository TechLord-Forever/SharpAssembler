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
    /// The CMPSQ (Compare Strings) instruction opcode.
    /// </summary>
    public class CmpsqOpcode : X86Opcode
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="CmpsqOpcode"/> class.
        /// </summary>
        public CmpsqOpcode()
            : base("cmpsq", GetOpcodeVariants())
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
                // CMPSQ
                new X86OpcodeVariant(
                    new byte[] { 0xA7 }, DataSize.Bit64),
            };
        }
    }
}

namespace SharpAssembler.Architectures.X86
{
    partial class Instr
    {
        /// <summary>
        /// Creates a new CMPSQ (Compare Strings) instruction.
        /// </summary>
        /// <returns>The created instruction.</returns>
        public static X86Instruction Cmpsq()
        {
            return X86Opcode.Cmpsq.CreateInstruction();
        }
    }

    partial class X86Opcode
    {
        /// <summary>
        /// The CMPSQ (Compare Strings) instruction opcode.
        /// </summary>
        public static readonly X86Opcode Cmpsq = new CmpsqOpcode();
    }
}

//////////////////////////////////////////////////////
//                     WARNING                      //
//     The contents of this file is generated.      //
//    DO NOT MODIFY, your changes will be lost!     //
//////////////////////////////////////////////////////
