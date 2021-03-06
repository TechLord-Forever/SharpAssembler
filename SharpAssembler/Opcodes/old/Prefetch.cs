﻿
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using SharpAssembler;
using SharpAssembler.Architectures.X86.Operands;

namespace SharpAssembler.Architectures.X86.Instructions
{
    /// <summary>
    /// The PREFETCH (Prefetch L1 Data-Cache Line) instruction.
    /// </summary>
    public class Prefetch : X86Instruction
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Prefetch"/> class.
        /// </summary>
        /// <param name="address">The address to prefetch.</param>
        public Prefetch(EffectiveAddress address)
        {
            #region Contract
            Contract.Requires<ArgumentNullException>(address != null);
            #endregion

            this.address = address;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the mnemonic of the instruction.
        /// </summary>
        /// <value>The mnemonic of the instruction.</value>
        public override string Mnemonic
        {
            get { return "prefetch"; }
        }

        private EffectiveAddress address;
        /// <summary>
        /// Gets the address to prefetch.
        /// </summary>
        /// <value>A <see cref="EffectiveAddress"/>.</value>
        public EffectiveAddress Address
        {
            get
            {
                #region Contract
                Contract.Ensures(Contract.Result<EffectiveAddress>() != null);
                #endregion
                return address;
            }
#if OPERAND_SET
            set
            {
                #region Contract
                Contract.Requires<ArgumentNullException>(value != null);
                #endregion
                address = value;
            }
#endif
        }
        #endregion

        #region Methods
        /// <summary>
        /// Enumerates an ordered list of operands used by this instruction.
        /// </summary>
        /// <returns>An <see cref="IEnumerable{T}"/> of <see cref="Operand"/> objects.</returns>
        public override IEnumerable<Operand> GetOperands()
        {
            // The order is important here!
            yield return this.address;
        }
        #endregion

        #region Instruction Variants
        /// <summary>
        /// An array of <see cref="X86OpcodeVariant"/> objects
        /// describing the possible variants of this instruction.
        /// </summary>
        private static X86OpcodeVariant[] variants = new[]{
            // PREFETCH mem8
            new X86OpcodeVariant(
                new byte[] { 0x0F, 0x0D }, 0,
                new OperandDescriptor(OperandType.MemoryOperand, DataSize.Bit8)),
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



