
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using SharpAssembler.Architectures.X86.Operands;

namespace SharpAssembler.Architectures.X86.Instructions
{
    /// <summary>
    /// The BSWAP (Byte Swap) instruction.
    /// </summary>
    public class Bswap : X86Instruction
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Bswap"/> class.
        /// </summary>
        /// <param name="subject">The register whose bytes will be swapped.</param>
        public Bswap(RegisterOperand subject)
        {
            #region Contract
            Contract.Requires<ArgumentNullException>(subject != null);
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
            get { return "bswap"; }
        }

        private RegisterOperand subject;
        /// <summary>
        /// Gets the register whose bytes are swapped.
        /// </summary>
        /// <value>A <see cref="RegisterOperand"/> operand.</value>
        public RegisterOperand Subject
        {
            get
            {
                #region Contract
                Contract.Ensures(Contract.Result<RegisterOperand>() != null);
                #endregion
                return subject;
            }
#if OPERAND_SET
            set
            {
                #region Contract
                Contract.Requires<ArgumentNullException>(value != null);
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
        }
        #endregion

        #region Instruction Variants
        /// <summary>
        /// An array of <see cref="X86Instruction.X86OpcodeVariant"/> objects
        /// describing the possible variants of this instruction.
        /// </summary>
        private static X86OpcodeVariant[] variants = new[]{
            // BSWAP reg32
            new X86OpcodeVariant(
                new byte[] { 0x0F, 0xC8 },
                new OperandDescriptor(OperandType.RegisterOperand, RegisterType.GeneralPurpose32Bit,
                    OperandEncoding.OpcodeAdd)),
            // BSWAP reg64
            new X86OpcodeVariant(
                new byte[] { 0x0F, 0xC8 },
                new OperandDescriptor(OperandType.RegisterOperand, RegisterType.GeneralPurpose64Bit,
                    OperandEncoding.OpcodeAdd)),
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
