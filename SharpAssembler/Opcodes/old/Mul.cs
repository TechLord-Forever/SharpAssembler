﻿
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using SharpAssembler.Architectures.X86.Operands;

namespace SharpAssembler.Architectures.X86.Instructions
{
    /// <summary>
    /// The MUL (Unsigned Multiply) instruction.
    /// </summary>
    public class Mul : X86Instruction
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Mul"/> class.
        /// </summary>
        /// <param name="multiplier">The multiplier.</param>
        public Mul(EffectiveAddress multiplier)
            : this((Operand)multiplier)
        {
            #region Contract
            Contract.Requires<ArgumentNullException>(multiplier != null);
            #endregion
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Mul"/> class.
        /// </summary>
        /// <param name="multiplier">The multiplier.</param>
        public Mul(RegisterOperand multiplier)
            : this((Operand)multiplier)
        {
            #region Contract
            Contract.Requires<ArgumentNullException>(multiplier != null);
            #endregion
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Mul"/> class.
        /// </summary>
        /// <param name="multiplier">The multiplier.</param>
        private Mul(Operand multiplier)
        {
            #region Contract
            Contract.Requires<ArgumentNullException>(multiplier != null);
            Contract.Requires<InvalidCastException>(
                    multiplier is EffectiveAddress ||
                    multiplier is RegisterOperand);
            #endregion

            this.multiplier = multiplier;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the mnemonic of the instruction.
        /// </summary>
        /// <value>The mnemonic of the instruction.</value>
        public override string Mnemonic
        {
            get { return "mul"; }
        }

        private Operand multiplier;
        /// <summary>
        /// Gets the multiplier.
        /// </summary>
        /// <value>An <see cref="Operand"/>.</value>
        public Operand Multiplier
        {
            get
            {
                #region Contract
                Contract.Ensures(Contract.Result<Operand>() != null);
                Contract.Ensures(
                    Contract.Result<Operand>() is EffectiveAddress ||
                    Contract.Result<Operand>() is RegisterOperand);
                #endregion
                return multiplier;
            }
#if OPERAND_SET
            set
            {
                #region Contract
                Contract.Requires<ArgumentNullException>(value != null);
                Contract.Requires<InvalidCastException>(
                    value is EffectiveAddress ||
                    value is RegisterOperand);
                #endregion
                multiplier = value;
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
            yield return this.multiplier;
        }
        #endregion

        #region Instruction Variants
        /// <summary>
        /// An array of <see cref="X86OpcodeVariant"/> objects
        /// describing the possible variants of this instruction.
        /// </summary>
        private static X86OpcodeVariant[] variants = new[]{
            // MUL reg/mem8
            new X86OpcodeVariant(
                new byte[] { 0xF6 }, 4,
                new OperandDescriptor(OperandType.RegisterOrMemoryOperand, RegisterType.GeneralPurpose8Bit)),
            // MUL reg/mem16
            new X86OpcodeVariant(
                new byte[] { 0xF7 }, 4,
                new OperandDescriptor(OperandType.RegisterOrMemoryOperand, RegisterType.GeneralPurpose16Bit)),
            // MUL reg/mem32
            new X86OpcodeVariant(
                new byte[] { 0xF7 }, 4,
                new OperandDescriptor(OperandType.RegisterOrMemoryOperand, RegisterType.GeneralPurpose32Bit)),
            // MUL reg/mem64
            new X86OpcodeVariant(
                new byte[] { 0xF7 }, 4,
                new OperandDescriptor(OperandType.RegisterOrMemoryOperand, RegisterType.GeneralPurpose64Bit)),
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
