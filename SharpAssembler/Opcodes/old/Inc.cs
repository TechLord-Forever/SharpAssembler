﻿
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using SharpAssembler.Architectures.X86.Operands;

namespace SharpAssembler.Architectures.X86.Instructions
{
    /// <summary>
    /// The INC (Increment by 1) instruction.
    /// </summary>
    public class Inc : X86Instruction, ILockInstruction
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Inc"/> class.
        /// </summary>
        /// <param name="subject">The subject memory operand.</param>
        public Inc(EffectiveAddress subject)
            : this((Operand)subject)
        {
            #region Contract
            Contract.Requires<ArgumentNullException>(subject != null);
            #endregion
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Inc"/> class.
        /// </summary>
        /// <param name="subject">The subject register operand.</param>
        public Inc(RegisterOperand subject)
            : this((Operand)subject)
        {
            #region Contract
            Contract.Requires<ArgumentNullException>(subject != null);
            #endregion
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Inc"/> class.
        /// </summary>
        /// <param name="subject">The subject operand.</param>
        private Inc(Operand subject)
        {
            #region Contract
            Contract.Requires<ArgumentNullException>(subject != null);
            Contract.Requires<InvalidCastException>(
                    subject is EffectiveAddress ||
                    subject is RegisterOperand);
            #endregion

            this.subject = subject;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the mnemonic of the instruction.
        /// </summary>
        /// <value>The mnemonic of the instruction.</value>
        public override string Mnemonic
        {
            get { return "inc"; }
        }

        private Operand subject;
        /// <summary>
        /// Gets the subject operand of the instruction.
        /// </summary>
        /// <value>An <see cref="Operand"/>.</value>
        public Operand Subject
        {
            get
            {
                #region Contract
                Contract.Ensures(Contract.Result<Operand>() != null);
                Contract.Ensures(
                    Contract.Result<Operand>() is EffectiveAddress ||
                    Contract.Result<Operand>() is RegisterOperand);
                #endregion
                return subject;
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
                subject = value;
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
            yield return this.subject;
        }
        #endregion

        #region Instruction Variants
        /// <summary>
        /// An array of <see cref="X86OpcodeVariant"/> objects
        /// describing the possible variants of this instruction.
        /// </summary>
        private static X86OpcodeVariant[] variants = new[]{
            // INC reg/mem8
            new X86OpcodeVariant(
                new byte[] { 0xFE }, 0,
                new OperandDescriptor(OperandType.RegisterOrMemoryOperand, RegisterType.GeneralPurpose8Bit)),
            // INC reg/mem16
            new X86OpcodeVariant(
                new byte[] { 0xFF }, 0,
                new OperandDescriptor(OperandType.RegisterOrMemoryOperand, RegisterType.GeneralPurpose16Bit)),
            // INC reg/mem32
            new X86OpcodeVariant(
                 new byte[] { 0xFF }, 0,
                new OperandDescriptor(OperandType.RegisterOrMemoryOperand, RegisterType.GeneralPurpose32Bit)),
            // INC reg/mem64
            new X86OpcodeVariant(
                 new byte[] { 0xFF }, 0,
                new OperandDescriptor(OperandType.RegisterOrMemoryOperand, RegisterType.GeneralPurpose64Bit)),

            // INC reg16
            new X86OpcodeVariant(
                new byte[] { 0x40 },
                new OperandDescriptor(OperandType.RegisterOperand, RegisterType.GeneralPurpose16Bit, OperandEncoding.OpcodeAdd)),
            // INC reg32
            new X86OpcodeVariant(
                new byte[] { 0x40 },
                new OperandDescriptor(OperandType.RegisterOperand, RegisterType.GeneralPurpose32Bit, OperandEncoding.OpcodeAdd)),
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
