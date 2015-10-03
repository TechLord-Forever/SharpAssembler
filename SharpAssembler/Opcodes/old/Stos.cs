
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using SharpAssembler;
using SharpAssembler.Architectures.X86.Operands;

namespace SharpAssembler.Architectures.X86.Instructions
{
    /// <summary>
    /// The STOS (Store String) instruction.
    /// </summary>
    public class Stos : X86Instruction
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Stos"/> class.
        /// </summary>
        /// <param name="size">The size of the data to store.</param>
        public Stos(DataSize size)
        {
            #region Contract
            Contract.Requires<InvalidEnumArgumentException>(Enum.IsDefined(typeof(DataSize), size));
            Contract.Requires<ArgumentException>(size == DataSize.Bit8 || size == DataSize.Bit16 ||
                size == DataSize.Bit32 || size == DataSize.Bit64,
                "The size must be either 8, 16, 32 or 64-bits.");
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
            get
            {
                switch (OperandSize)
                {
                    case DataSize.Bit8:
                        return "stosb";
                    case DataSize.Bit16:
                        return "stosw";
                    case DataSize.Bit32:
                        return "stosd";
                    case DataSize.Bit64:
                        return "stosq";
                    default:
                        throw new Exception();
                }
            }
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
            // STOSB
            new X86OpcodeVariant(
                new byte[] { 0xAA }, DataSize.Bit8),
            // STOSW
            new X86OpcodeVariant(
                new byte[] { 0xAB }, DataSize.Bit16),
            // STOSD
            new X86OpcodeVariant(
                new byte[] { 0xAB }, DataSize.Bit32),
            // STOSQ
            new X86OpcodeVariant(
                new byte[] { 0xAB }, DataSize.Bit64),
        };

        /// <summary>
        /// Returns an array containing the <see cref="X86OpcodeVariant"/>
        /// objects representing all the possible variants of this instruction.
        /// </summary>
        /// <returns>An array of <see cref="X86OpcodeVariant"/>
        /// objects.</returns>
        internal override X86OpcodeVariant[] GetVariantList()
        {
            return variants;
        }
        #endregion
    }
}
