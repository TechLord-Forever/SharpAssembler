﻿
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using SharpAssembler;
using SharpAssembler.Architectures.X86.Operands;

namespace SharpAssembler.Architectures.X86.Instructions
{
    /// <summary>
    /// The RETF (Far Return from Called Procedure) instruction.
    /// </summary>
    public class Retf : X86Instruction
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Retf"/> class.
        /// </summary>
        public Retf()
            : this(null)
        { /* Nothing to do. */ }

        /// <summary>
        /// Initializes a new instance of the <see cref="Retf"/> class.
        /// </summary>
        /// <param name="popcount">The number of bytes to pop from the stack after returning.</param>
        private Retf(Immediate popcount)
        {
            this.popcount = popcount;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the mnemonic of the instruction.
        /// </summary>
        /// <value>The mnemonic of the instruction.</value>
        public override string Mnemonic
        {
            get { return "retf"; }
        }

        private Immediate popcount;
        /// <summary>
        /// Gets the number of bytes to pop from the stack after returning.
        /// </summary>
        /// <value>An <see cref="Immediate"/> value; or <see langword="null"/>.</value>
        public Immediate PopCount
        {
            get { return popcount; }
            set { popcount = value; }
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
            yield return this.popcount;
        }
        #endregion

        #region Instruction Variants
        /// <summary>
        /// An array of <see cref="X86OpcodeVariant"/> objects
        /// describing the possible variants of this instruction.
        /// </summary>
        private static X86OpcodeVariant[] variants = new[]{
            // RETF
            new X86OpcodeVariant(
                new byte[] { 0xCB },
                new OperandDescriptor(OperandType.None, DataSize.None)),
            // RETF imm16
            new X86OpcodeVariant(
                new byte[] { 0xCA },
                new OperandDescriptor(OperandType.Immediate, DataSize.Bit16)),
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