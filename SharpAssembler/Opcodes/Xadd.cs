
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using SharpAssembler.Architectures.X86.Operands;

namespace SharpAssembler.Architectures.X86.Instructions
{
    /// <summary>
    /// The XADD (Exchange and Add) instruction.
    /// </summary>
    public class Xadd : X86Instruction, ILockInstruction
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Xadd"/> class.
        /// </summary>
        /// <param name="destination">The destination register operand.</param>
        /// <param name="source">The source immediate register operand.</param>
        public Xadd(RegisterOperand destination, RegisterOperand source)
            : this((Operand)destination, source)
        {
            #region Contract
            Contract.Requires<ArgumentNullException>(destination != null);
            Contract.Requires<ArgumentNullException>(source != null);
            #endregion
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Xadd"/> class.
        /// </summary>
        /// <param name="destination">The destination memory operand.</param>
        /// <param name="source">The source immediate register operand.</param>
        public Xadd(EffectiveAddress destination, RegisterOperand source)
            : this((Operand)destination, source)
        {
            #region Contract
            Contract.Requires<ArgumentNullException>(destination != null);
            Contract.Requires<ArgumentNullException>(source != null);
            #endregion
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Xadd"/> class.
        /// </summary>
        /// <param name="destination">The destination operand.</param>
        /// <param name="source">The source operand.</param>
        private Xadd(Operand destination, RegisterOperand source)
        {
            #region Contract
            Contract.Requires<ArgumentNullException>(destination != null);
            Contract.Requires<InvalidCastException>(
                    destination is EffectiveAddress ||
                    destination is RegisterOperand);
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
            get { return "xadd"; }
        }

        private RegisterOperand source;
        /// <summary>
        /// Gets the source operand of the instruction.
        /// </summary>
        /// <value>A <see cref="RegisterOperand"/>.</value>
        public RegisterOperand Source
        {
            get
            {
                #region Contract
                Contract.Ensures(Contract.Result<Operand>() != null);
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

        private Operand destination;
        /// <summary>
        /// Gets the destination operand of the instruction.
        /// </summary>
        /// <value>An <see cref="Operand"/>.</value>
        public Operand Destination
        {
            get
            {
                #region Contract
                Contract.Ensures(Contract.Result<Operand>() != null);
                Contract.Ensures(
                    Contract.Result<Operand>() is EffectiveAddress ||
                    Contract.Result<Operand>() is RegisterOperand);
                #endregion
                return destination;
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
            yield return this.source;
        }
        #endregion

        #region Instruction Variants
        /// <summary>
        /// An array of <see cref="X86OpcodeVariant"/> objects
        /// describing the possible variants of this instruction.
        /// </summary>
        private static X86OpcodeVariant[] variants = new[]{
            // XADD reg/mem8, reg8
            new X86OpcodeVariant(
                new byte[] { 0x0F, 0xC0 },
                new OperandDescriptor(OperandType.RegisterOrMemoryOperand, RegisterType.GeneralPurpose8Bit),
                new OperandDescriptor(OperandType.RegisterOperand, RegisterType.GeneralPurpose8Bit)),
            // XADD reg/mem16, reg16
            new X86OpcodeVariant(
                new byte[] { 0x0F, 0xC1 },
                new OperandDescriptor(OperandType.RegisterOrMemoryOperand, RegisterType.GeneralPurpose16Bit),
                new OperandDescriptor(OperandType.RegisterOperand, RegisterType.GeneralPurpose16Bit)),
            // XADD reg/mem32, reg32
            new X86OpcodeVariant(
                new byte[] { 0x0F, 0xC1 },
                new OperandDescriptor(OperandType.RegisterOrMemoryOperand, RegisterType.GeneralPurpose32Bit),
                new OperandDescriptor(OperandType.RegisterOperand, RegisterType.GeneralPurpose32Bit)),
            // XADD reg/mem64, reg64
            new X86OpcodeVariant(
                new byte[] { 0x0F, 0xC1 },
                new OperandDescriptor(OperandType.RegisterOrMemoryOperand, RegisterType.GeneralPurpose64Bit),
                new OperandDescriptor(OperandType.RegisterOperand, RegisterType.GeneralPurpose64Bit)),
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
