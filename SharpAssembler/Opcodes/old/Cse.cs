﻿
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using SharpAssembler;
using SharpAssembler.Architectures.X86.Operands;

namespace SharpAssembler.Architectures.X86.Instructions
{
    /// <summary>
    /// The CSE (Convert to Sign-Extended) instruction.
    /// </summary>
    /// <remarks>
    /// This instruction represents the <c>CBW</c>, <c>CWDE</c>, <c>CDQE</c>, <c>CWD</c>, <c>CDQ</c> and <c>CQO</c>
    /// instructions.
    /// </remarks>
    public class Cse : X86Instruction
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Cse"/> class.
        /// </summary>
        public Cse()
        {
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the mnemonic of the instruction.
        /// </summary>
        /// <value>The mnemonic of the instruction.</value>
        public override string Mnemonic
        {
            get { return "cse"; }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Enumerates an ordered list of operands used by this instruction.
        /// </summary>
        /// <returns>An <see cref="IEnumerable{T}"/> of <see cref="Operand"/> objects.</returns>
        public override IEnumerable<Operand> GetOperands()
        {
            yield break;
        }
        #endregion

        #region Instruction Variants
        /// <summary>
        /// An array of <see cref="X86OpcodeVariant"/> objects
        /// describing the possible variants of this instruction.
        /// </summary>
        private static X86OpcodeVariant[] variants = new[]{
            // CBW
            new X86OpcodeVariant(
                new byte[] { 0x98 }, DataSize.Bit16),
            // CWDE
            new X86OpcodeVariant(
                new byte[] { 0x98 }, DataSize.Bit32),
            // CDQE
            new X86OpcodeVariant(
                new byte[] { 0x98 }, DataSize.Bit64),

            // CWD
            new X86OpcodeVariant(
                new byte[] { 0x99 }, DataSize.Bit16),
            // CDQ
            new X86OpcodeVariant(
                new byte[] { 0x99 }, DataSize.Bit32),
            // CQO
            new X86OpcodeVariant(
                new byte[] { 0x99 }, DataSize.Bit64),
        };

        /// <summary>
        /// Returns an array containing the <see cref="X86OpcodeVariant"/>
        /// objects representing all the possible variants of this instruction.
        /// </summary>
        /// <returns>An array of <see cref="X86OpcodeVariant"/>
        /// objects.</returns>
        internal override X86OpcodeVariant[] GetVariantList()
        { return variants; }
        #endregion
    }
}
