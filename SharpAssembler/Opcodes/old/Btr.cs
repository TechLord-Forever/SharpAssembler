﻿
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using SharpAssembler;
using SharpAssembler.Architectures.X86.Operands;

namespace SharpAssembler.Architectures.X86.Instructions
{
    /// <summary>
    /// The BTR (Bit Test and Reset) instruction.
    /// </summary>
    public class Btr : X86Instruction
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Btr"/> class.
        /// </summary>
        /// <param name="subject">The register operand whose bit is copied and reset.</param>
        /// <param name="bitIndex">The index of the bit to copy.</param>
        public Btr(RegisterOperand subject, RegisterOperand bitIndex)
            : this((Operand)subject, (Operand)bitIndex)
        {
            #region Contract
            Contract.Requires<ArgumentNullException>(subject != null);
            Contract.Requires<ArgumentNullException>(bitIndex != null);
            #endregion
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Btr"/> class.
        /// </summary>
        /// <param name="subject">The memory operand whose bit is copied and reset.</param>
        /// <param name="bitIndex">The index of the bit to copy.</param>
        public Btr(EffectiveAddress subject, RegisterOperand bitIndex)
            : this((Operand)subject, (Operand)bitIndex)
        {
            #region Contract
            Contract.Requires<ArgumentNullException>(subject != null);
            Contract.Requires<ArgumentNullException>(bitIndex != null);
            #endregion
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Btr"/> class.
        /// </summary>
        /// <param name="subject">The register operand whose bit is copied and reset.</param>
        /// <param name="bitIndex">The index of the bit to copy.</param>
        public Btr(RegisterOperand subject, Immediate bitIndex)
            : this((Operand)subject, (Operand)bitIndex)
        {
            #region Contract
            Contract.Requires<ArgumentNullException>(subject != null);
            Contract.Requires<ArgumentNullException>(bitIndex != null);
            #endregion
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Btr"/> class.
        /// </summary>
        /// <param name="subject">The memory operand whose bit is copied and reset.</param>
        /// <param name="bitIndex">The index of the bit to copy.</param>
        public Btr(EffectiveAddress subject, Immediate bitIndex)
            : this((Operand)subject, (Operand)bitIndex)
        {
            #region Contract
            Contract.Requires<ArgumentNullException>(subject != null);
            Contract.Requires<ArgumentNullException>(bitIndex != null);
            #endregion
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Btr"/> class.
        /// </summary>
        /// <param name="subject">The register or memory operand whose bit is copied and reset.</param>
        /// <param name="bitIndex">The index of the bit to copy.</param>
        private Btr(Operand subject, Operand bitIndex)
        {
            #region Contract
            Contract.Requires<ArgumentNullException>(subject != null);
            Contract.Requires<InvalidCastException>(
                subject is RegisterOperand ||
                subject is EffectiveAddress);
            Contract.Requires<ArgumentNullException>(bitIndex != null);
            Contract.Requires<InvalidCastException>(
                bitIndex is RegisterOperand ||
                bitIndex is Immediate);
            #endregion

            this.subject = subject;
            this.bitIndex = bitIndex;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the mnemonic of the instruction.
        /// </summary>
        /// <value>The mnemonic of the instruction.</value>
        public override string Mnemonic
        {
            get { return "btr"; }
        }

        private Operand bitIndex;
        /// <summary>
        /// Gets the index of the bit to copy.
        /// </summary>
        /// <value>An <see cref="Operand"/>.</value>
        public Operand BitIndex
        {
            get
            {
                #region Contract
                Contract.Ensures(Contract.Result<Operand>() != null);
                Contract.Ensures(
                    Contract.Result<Operand>() is RegisterOperand ||
                    Contract.Result<Operand>() is Immediate);
                #endregion
                return bitIndex;
            }
#if OPERAND_SET
            set
            {
                #region Contract
                Contract.Requires<ArgumentNullException>(value != null);
                Contract.Requires<InvalidCastException>(
                    value is RegisterOperand ||
                    value is Immediate);
                #endregion
                bitIndex = value;
            }
#endif
        }

        private Operand subject;
        /// <summary>
        /// Gets the register or memory operand from which the bit is copied and reset.
        /// </summary>
        /// <value>An <see cref="Operand"/>.</value>
        public Operand Subject
        {
            get
            {
                #region Contract
                Contract.Ensures(Contract.Result<Operand>() != null);
                Contract.Ensures(
                    Contract.Result<Operand>() is RegisterOperand ||
                    Contract.Result<Operand>() is EffectiveAddress);
                #endregion
                return subject;
            }
#if OPERAND_SET
            set
            {
                #region Contract
                Contract.Requires<ArgumentNullException>(value != null);
                Contract.Requires<InvalidCastException>(
                    value is RegisterOperand ||
                    value is EffectiveAddress);
                #endregion
                subject = value;
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
            yield return this.subject;
            yield return this.bitIndex;
        }
        #endregion

        #region Instruction Variants
        /// <summary>
        /// An array of <see cref="X86OpcodeVariant"/> objects
        /// describing the possible variants of this instruction.
        /// </summary>
        private static X86OpcodeVariant[] variants = new[]{
            // BTR reg/mem16, reg16
            new X86OpcodeVariant(
                new byte[] { 0x0F, 0xB3 },
                new OperandDescriptor(OperandType.RegisterOrMemoryOperand, RegisterType.GeneralPurpose16Bit),
                new OperandDescriptor(OperandType.RegisterOperand, RegisterType.GeneralPurpose16Bit)),
            // BTR reg/mem32, reg32
            new X86OpcodeVariant(
                new byte[] { 0x0F, 0xB3 },
                new OperandDescriptor(OperandType.RegisterOrMemoryOperand, RegisterType.GeneralPurpose32Bit),
                new OperandDescriptor(OperandType.RegisterOperand, RegisterType.GeneralPurpose32Bit)),
            // BTR reg/mem64, reg64
            new X86OpcodeVariant(
                new byte[] { 0x0F, 0xB3 },
                new OperandDescriptor(OperandType.RegisterOrMemoryOperand, RegisterType.GeneralPurpose64Bit),
                new OperandDescriptor(OperandType.RegisterOperand, RegisterType.GeneralPurpose64Bit)),

            // BTR reg/mem16, imm8
            new X86OpcodeVariant(
                new byte[] { 0x0F, 0xBA }, 6,
                new OperandDescriptor(OperandType.RegisterOrMemoryOperand, RegisterType.GeneralPurpose16Bit),
                new OperandDescriptor(OperandType.Immediate, DataSize.Bit8)),
            // BTR reg/mem32, imm8
            new X86OpcodeVariant(
                new byte[] { 0x0F, 0xBA }, 6,
                new OperandDescriptor(OperandType.RegisterOrMemoryOperand, RegisterType.GeneralPurpose32Bit),
                new OperandDescriptor(OperandType.Immediate, DataSize.Bit8)),
            // BTR reg/mem64, imm8
            new X86OpcodeVariant(
                new byte[] { 0x0F, 0xBA }, 6,
                new OperandDescriptor(OperandType.RegisterOrMemoryOperand, RegisterType.GeneralPurpose64Bit),
                new OperandDescriptor(OperandType.Immediate, DataSize.Bit8)),
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