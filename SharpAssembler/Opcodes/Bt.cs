
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using SharpAssembler;
using SharpAssembler.Architectures.X86.Operands;

namespace SharpAssembler.Architectures.X86.Instructions
{
    /// <summary>
    /// The BT (Bit Test) instruction.
    /// </summary>
    public class Bt : X86Instruction
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Bt"/> class.
        /// </summary>
        /// <param name="subject">The register operand whose bit is copied.</param>
        /// <param name="bitindex">The index of the bit to copy.</param>
        public Bt(RegisterOperand subject, RegisterOperand bitindex)
            : this((Operand)subject, (Operand)bitindex)
        {
            #region Contract
            Contract.Requires<ArgumentNullException>(subject != null);
            Contract.Requires<ArgumentNullException>(bitindex != null);
            #endregion
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Bt"/> class.
        /// </summary>
        /// <param name="subject">The memory operand whose bit is copied.</param>
        /// <param name="bitindex">The index of the bit to copy.</param>
        public Bt(EffectiveAddress subject, RegisterOperand bitindex)
            : this((Operand)subject, (Operand)bitindex)
        {
            #region Contract
            Contract.Requires<ArgumentNullException>(subject != null);
            Contract.Requires<ArgumentNullException>(bitindex != null);
            #endregion
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Bt"/> class.
        /// </summary>
        /// <param name="subject">The register operand whose bit is copied.</param>
        /// <param name="bitindex">The index of the bit to copy.</param>
        public Bt(RegisterOperand subject, Immediate bitindex)
            : this((Operand)subject, (Operand)bitindex)
        {
            #region Contract
            Contract.Requires<ArgumentNullException>(subject != null);
            Contract.Requires<ArgumentNullException>(bitindex != null);
            #endregion
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Bt"/> class.
        /// </summary>
        /// <param name="subject">The memory operand whose bit is copied.</param>
        /// <param name="bitindex">The index of the bit to copy.</param>
        public Bt(EffectiveAddress subject, Immediate bitindex)
            : this((Operand)subject, (Operand)bitindex)
        {
            #region Contract
            Contract.Requires<ArgumentNullException>(subject != null);
            Contract.Requires<ArgumentNullException>(bitindex != null);
            #endregion
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Bt"/> class.
        /// </summary>
        /// <param name="subject">The register or memory operand whose bit is copied.</param>
        /// <param name="bitindex">The index of the bit to copy.</param>
        private Bt(Operand subject, Operand bitindex)
        {
            #region Contract
            Contract.Requires<ArgumentNullException>(subject != null);
            Contract.Requires<InvalidCastException>(
                    subject is EffectiveAddress ||
                    subject is RegisterOperand);
            Contract.Requires<ArgumentNullException>(bitindex != null);
            Contract.Requires<InvalidCastException>(
                    bitindex is Immediate ||
                    bitindex is RegisterOperand);
            #endregion

            this.subject = subject;
            this.bitIndex = bitindex;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the mnemonic of the instruction.
        /// </summary>
        /// <value>The mnemonic of the instruction.</value>
        public override string Mnemonic
        {
            get { return "bt"; }
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
                    Contract.Result<Operand>() is Immediate ||
                    Contract.Result<Operand>() is RegisterOperand);
                #endregion
                return bitIndex;
            }
#if OPERAND_SET
            set
            {
                #region Contract
                Contract.Requires<ArgumentNullException>(value != null);
                Contract.Requires<InvalidCastException>(
                    value is Immediate ||
                    value is RegisterOperand);
                #endregion
                bitIndex = value;
            }
#endif
        }

        private Operand subject;
        /// <summary>
        /// Gets the register or memory operand from which the bit is copied.
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
            yield return this.subject;
            yield return this.bitIndex;
        }
        #endregion

        #region Instruction Variants
        /// <summary>
        /// An array of <see cref="X86Instruction.X86OpcodeVariant"/> objects
        /// describing the possible variants of this instruction.
        /// </summary>
        private static X86OpcodeVariant[] variants = new[]
        {
            // BT reg/mem16, reg16
            new X86OpcodeVariant(
                new byte[] { 0x0F, 0xA3 },
                new OperandDescriptor(OperandType.RegisterOrMemoryOperand, RegisterType.GeneralPurpose16Bit),
                new OperandDescriptor(OperandType.RegisterOperand, RegisterType.GeneralPurpose16Bit)),
            // BT reg/mem32, reg32
            new X86OpcodeVariant(
                new byte[] { 0x0F, 0xA3 },
                new OperandDescriptor(OperandType.RegisterOrMemoryOperand, RegisterType.GeneralPurpose32Bit),
                new OperandDescriptor(OperandType.RegisterOperand, RegisterType.GeneralPurpose32Bit)),
            // BT reg/mem64, reg64
            new X86OpcodeVariant(
                new byte[] { 0x0F, 0xA3 },
                new OperandDescriptor(OperandType.RegisterOrMemoryOperand, RegisterType.GeneralPurpose64Bit),
                new OperandDescriptor(OperandType.RegisterOperand, RegisterType.GeneralPurpose64Bit)),

            // BT reg/mem16, imm8
            new X86OpcodeVariant(
                new byte[] { 0x0F, 0xBA }, 4,
                new OperandDescriptor(OperandType.RegisterOrMemoryOperand, RegisterType.GeneralPurpose16Bit),
                new OperandDescriptor(OperandType.Immediate, DataSize.Bit8)),
            // BT reg/mem32, imm8
            new X86OpcodeVariant(
                new byte[] { 0x0F, 0xBA }, 4,
                new OperandDescriptor(OperandType.RegisterOrMemoryOperand, RegisterType.GeneralPurpose32Bit),
                new OperandDescriptor(OperandType.Immediate, DataSize.Bit8)),
            // BT reg/mem64, imm8
            new X86OpcodeVariant(
                new byte[] { 0x0F, 0xBA }, 4,
                new OperandDescriptor(OperandType.RegisterOrMemoryOperand, RegisterType.GeneralPurpose64Bit),
                new OperandDescriptor(OperandType.Immediate, DataSize.Bit8)),
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
