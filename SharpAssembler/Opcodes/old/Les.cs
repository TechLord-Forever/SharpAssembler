﻿
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using SharpAssembler;
using SharpAssembler.Architectures.X86.Operands;

namespace SharpAssembler.Architectures.X86.Instructions
{
    /// <summary>
    /// The LES (Load Far Pointer) instruction.
    /// </summary>
    public class Les : X86Instruction
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Les"/> class.
        /// </summary>
        /// <param name="destination">The destination operand.</param>
        /// <param name="source">The source operand.</param>
        public Les(RegisterOperand destination, EffectiveAddress source)
        {
            #region Contract
            Contract.Requires<ArgumentNullException>(destination != null);
            Contract.Requires<ArgumentNullException>(source != null);
            #endregion

            this.destination = destination;
            this.source = source;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the mnemonic of the instruction.
        /// </summary>
        /// <value>The mnemonic of the instruction.</value>
        public override string Mnemonic
        {
            get { return "les"; }
        }

        /// <summary>
        /// Gets whether this instruction is valid in 64-bit mode.
        /// </summary>
        /// <value><see langword="true"/> when the instruction is valid in 64-bit mode;
        /// otherwise, <see langword="false"/>.</value>
        public override bool ValidIn64BitMode
        {
            get { return false; }
        }

        private RegisterOperand destination;
        /// <summary>
        /// Gets the destination operand of the instruction.
        /// </summary>
        /// <value>An <see cref="RegisterOperand"/>.</value>
        public RegisterOperand Destination
        {
            get
            {
                #region Contract
                Contract.Ensures(Contract.Result<RegisterOperand>() != null);
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

        private EffectiveAddress source;
        /// <summary>
        /// Gets the source operand of the instruction.
        /// </summary>
        /// <value>An <see cref="EffectiveAddress"/>.</value>
        public EffectiveAddress Source
        {
            get
            {
                #region Contract
                Contract.Ensures(Contract.Result<EffectiveAddress>() != null);
                #endregion
                return source;
            }
#if OPERAND_SET
            set
            {
                #region Contract
                Contract.Requires<ArgumentNullException>(value != null);
                #endregion
                source = value;
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
            yield return this.destination;
            yield return this.source;
        }
        #endregion

        #region Instruction Variants
        /// <summary>
        /// An array of <see cref="X86OpcodeVariant"/> objects
        /// describing the possible variants of this instruction.
        /// </summary>
        private static X86OpcodeVariant[] variants = new[]{
            // LES reg16, mem16:16
            new X86OpcodeVariant(
                new byte[] { 0xC4 },
                new OperandDescriptor(OperandType.RegisterOperand, DataSize.Bit16),
                new OperandDescriptor(OperandType.MemoryOperand, DataSize.Bit16)),
            // LES reg32, mem16:32
            new X86OpcodeVariant(
                new byte[] { 0xC4 },
                new OperandDescriptor(OperandType.RegisterOperand, DataSize.Bit32),
                new OperandDescriptor(OperandType.MemoryOperand, DataSize.Bit32)),
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
