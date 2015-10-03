
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using SharpAssembler.Architectures.X86.Operands;

namespace SharpAssembler.Architectures.X86.Instructions
{
    /// <summary>
    /// The BSR (Bit Scan Reverse) instruction.
    /// </summary>
    public class Bsr : X86Instruction
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Bsr"/> class.
        /// </summary>
        /// <param name="destination">The register in which the bit's index will be stored.</param>
        /// <param name="subject">The register operand which is checked.</param>
        public Bsr(RegisterOperand destination, RegisterOperand subject)
            : this(destination, (Operand)subject)
        {
            #region Contract
            Contract.Requires<ArgumentNullException>(destination != null);
            Contract.Requires<ArgumentNullException>(subject != null);
            #endregion
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Bsr"/> class.
        /// </summary>
        /// <param name="destination">The register in which the bit's index will be stored.</param>
        /// <param name="subject">The memory operand which is checked.</param>
        public Bsr(RegisterOperand destination, EffectiveAddress subject)
            : this(destination, (Operand)subject)
        {
            #region Contract
            Contract.Requires<ArgumentNullException>(destination != null);
            Contract.Requires<ArgumentNullException>(subject != null);
            #endregion
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Bsr"/> class.
        /// </summary>
        /// <param name="destination">The register in which the bit's index will be stored.</param>
        /// <param name="subject">The register or memory operand which is checked.</param>
        private Bsr(RegisterOperand destination, Operand subject)
        {
            #region Contract
            Contract.Requires<ArgumentNullException>(destination != null);
            Contract.Requires<InvalidCastException>(
                    subject is EffectiveAddress ||
                    subject is RegisterOperand);
            Contract.Requires<ArgumentNullException>(subject != null);
            #endregion

            this.destination = destination;
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
            get { return "bsr"; }
        }

        private RegisterOperand destination;
        /// <summary>
        /// Gets the register where the bit index is stored.
        /// </summary>
        /// <value>A <see cref="RegisterOperand"/> operand.</value>
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

        private Operand subject;
        /// <summary>
        /// Gets the subject register or memory operand.
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
            yield return this.subject;
        }
        #endregion

        #region Instruction Variants
        /// <summary>
        /// An array of <see cref="X86Instruction.X86OpcodeVariant"/> objects
        /// describing the possible variants of this instruction.
        /// </summary>
        private static X86OpcodeVariant[] variants = new[]{
            // BSR reg16, reg/mem16
            new X86OpcodeVariant(
                new byte[] { 0x0F, 0xBD },
                new OperandDescriptor(OperandType.RegisterOperand, RegisterType.GeneralPurpose16Bit),
                new OperandDescriptor(OperandType.RegisterOrMemoryOperand, RegisterType.GeneralPurpose16Bit)),
            // BSR reg32, reg/mem32
            new X86OpcodeVariant(
                new byte[] { 0x0F, 0xBD },
                new OperandDescriptor(OperandType.RegisterOperand, RegisterType.GeneralPurpose32Bit),
                new OperandDescriptor(OperandType.RegisterOrMemoryOperand, RegisterType.GeneralPurpose32Bit)),
            // BSR reg64, reg/mem64
            new X86OpcodeVariant(
                new byte[] { 0x0F, 0xBD },
                new OperandDescriptor(OperandType.RegisterOperand, RegisterType.GeneralPurpose64Bit),
                new OperandDescriptor(OperandType.RegisterOrMemoryOperand, RegisterType.GeneralPurpose64Bit)),
        };

        /// <summary>
        /// Returns an array containing the <see cref="X86Instruction.X86OpcodeVariant"/>
        /// objects representing all the possible variants of this instruction.
        /// </summary>
        /// <returns>An array of <see cref="X86Instruction.X86OpcodeVariant"/>
        /// objects.</returns>
        internal override X86OpcodeVariant[] GetVariantList()
        { return variants; }
        #endregion
    }
}
