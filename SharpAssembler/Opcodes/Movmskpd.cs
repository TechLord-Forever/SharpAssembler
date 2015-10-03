
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using SharpAssembler.Architectures.X86.Operands;

namespace SharpAssembler.Architectures.X86.Instructions
{
    /// <summary>
    /// The MOVMSKPD (Extract Packed Double-Precision Floating-Point Sign Mask) instruction.
    /// </summary>
    public class Movmskpd : X86Instruction
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Movmskpd"/> class.
        /// </summary>
        /// <param name="destination">The destination register.</param>
        /// <param name="source">The source register.</param>
        public Movmskpd(RegisterOperand destination, RegisterOperand source)
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
            get { return "movmskpd"; }
        }

        private RegisterOperand source;
        /// <summary>
        /// Gets the source operand of the instruction.
        /// </summary>
        /// <value>An <see cref="RegisterOperand"/>.</value>
        public RegisterOperand Source
        {
            get
            {
                #region Contract
                Contract.Ensures(Contract.Result<RegisterOperand>() != null);
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
                return Destination;
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
            // MOVMSKPD reg32, xmm
            new X86OpcodeVariant(
                new byte[] { 0x66, 0x0F, 0x50 },
                new OperandDescriptor(OperandType.RegisterOperand, RegisterType.GeneralPurpose32Bit),
                new OperandDescriptor(OperandType.RegisterOperand, RegisterType.Simd128Bit)),
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
