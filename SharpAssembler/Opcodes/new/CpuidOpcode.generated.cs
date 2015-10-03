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
    /// The CPUID (Processor Identification) instruction opcode.
    /// </summary>
    public class CpuidOpcode : X86Opcode
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="CpuidOpcode"/> class.
        /// </summary>
        public CpuidOpcode()
            : base("cpuid", GetOpcodeVariants())
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
                // CPUID
                new X86OpcodeVariant(
                    new byte[] { 0x0F, 0xA2 }),
            };
        }
    }
}

namespace SharpAssembler.Architectures.X86
{
    partial class Instr
    {
        /// <summary>
        /// Creates a new CPUID (Processor Identification) instruction.
        /// </summary>
        /// <returns>The created instruction.</returns>
        public static X86Instruction Cpuid()
        {
            return X86Opcode.Cpuid.CreateInstruction();
        }
    }

    partial class X86Opcode
    {
        /// <summary>
        /// The CPUID (Processor Identification) instruction opcode.
        /// </summary>
        public static readonly X86Opcode Cpuid = new CpuidOpcode();
    }
}

//////////////////////////////////////////////////////
//                     WARNING                      //
//     The contents of this file is generated.      //
//    DO NOT MODIFY, your changes will be lost!     //
//////////////////////////////////////////////////////
