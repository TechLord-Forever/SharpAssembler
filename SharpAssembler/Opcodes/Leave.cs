
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using SharpAssembler;
using SharpAssembler.Architectures.X86.Operands;

namespace SharpAssembler.Architectures.X86.Instructions
{
    /// <summary>
    /// The LEAVE (Delete Procedure Stack Frame) instruction.
    /// </summary>
    public class Leave : X86Instruction
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Leave"/> class.
        /// </summary>
        /// <param name="size">The size of the data to compare.</param>
        public Leave(DataSize size)
        {
            #region Contract
            Contract.Requires<InvalidEnumArgumentException>(Enum.IsDefined(typeof(DataSize), size));
            Contract.Requires<ArgumentException>(size == DataSize.Bit16 || size == DataSize.Bit32 || size == DataSize.Bit64,
                "The size must be either 16, 32 or 64-bits.");
            #endregion

            this.OperandSize = size;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the mnemonic of the instruction.
        /// </summary>
        /// <value>The mnemonic of the instruction.</value>
        public override string Mnemonic
        {
            get { return "leave"; }
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
            // LEAVE
            new X86OpcodeVariant(
                new byte[] { 0xC9 }, DataSize.Bit16),
            // LEAVE
            new X86OpcodeVariant(
                new byte[] { 0xC9 }, DataSize.Bit32),
            // LEAVE
            new X86OpcodeVariant(
                new byte[] { 0xC9 }, DataSize.Bit64),
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
