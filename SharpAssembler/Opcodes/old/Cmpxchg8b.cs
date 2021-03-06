﻿
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using SharpAssembler;
using SharpAssembler.Architectures.X86.Operands;

namespace SharpAssembler.Architectures.X86.Instructions
{
    /// <summary>
    /// The CMPXCHG8B (Compare and Exchange Eight Bytes) instruction.
    /// </summary>
    public class Cmpxchg8b : X86Instruction, ILockInstruction
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Cmpxchg8b"/> class.
        /// </summary>
        /// <param name="destination">The operand being compared and written to.</param>
        public Cmpxchg8b(EffectiveAddress destination)
        {
            #region Contract
            Contract.Requires<ArgumentNullException>(destination != null);
            #endregion

            this.destination = destination;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the mnemonic of the instruction.
        /// </summary>
        /// <value>The mnemonic of the instruction.</value>
        public override string Mnemonic
        {
            get { return "cmpxchg8b"; }
        }

        private EffectiveAddress destination;
        /// <summary>
        /// Gets the operand which is compared and written to.
        /// </summary>
        /// <value>An <see cref="EffectiveAddress"/>.</value>
        public EffectiveAddress Destination
        {
            get
            {
                #region Contract
                Contract.Ensures(Contract.Result<EffectiveAddress>() != null);
                #endregion
                return destination;
            }
#if OPERAND_SET
            set
            {
            #region Contract
                Contract.Requires<ArgumentNullException>(value != null);
                #endregion
                destination = value;
            }
#endif
        }

        private bool lockInstruction = false;
        /// <summary>
        /// Gets or sets whether the lock prefix is used.
        /// </summary>
        /// <value><see langword="true"/> to enable the lock prefix; otherwise, <see langword="false"/>.
        /// The default is <see langword="false"/>.</value>
        /// <remarks>
        /// When this property is set to <see langword="true"/>, the lock signal is asserted before accessing the
        /// specified memory location. When the lock signal has already been asserted, the instruction must wait for it
        /// to be released. Instructions without the lock prefix do not check the lock signal, and will be executed
        /// even when the lock signal is asserted by some other instruction.
        /// </remarks>
        public bool Lock
        {
            get { return lockInstruction; }
            set { lockInstruction = value; }
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
            yield return this.destination;
        }
        #endregion

        #region Instruction Variants
        /// <summary>
        /// An array of <see cref="X86OpcodeVariant"/> objects
        /// describing the possible variants of this instruction.
        /// </summary>
        private static X86OpcodeVariant[] variants = new[]{
            // CMPXCHG8B mem64
            new X86OpcodeVariant(
                new byte[] { 0x0F, 0xC7 }, 1,
                new OperandDescriptor(OperandType.MemoryOperand, DataSize.Bit64)),
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
