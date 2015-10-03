
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using SharpAssembler;
using SharpAssembler.Architectures.X86.Operands;

namespace SharpAssembler.Architectures.X86.Instructions
{
    /// <summary>
    /// The MOVNTI (Move Non-Temporal Doubleword or Quadword) instruction.
    /// </summary>
    public class Movnti : X86Instruction
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Movnti"/> class.
        /// </summary>
        /// <param name="destination">The destination memory operand.</param>
        /// <param name="source">The source register.</param>
        public Movnti(EffectiveAddress destination, RegisterOperand source)
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
            get { return "movnti"; }
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

        private EffectiveAddress destination;
        /// <summary>
        /// Gets the destination operand of the instruction.
        /// </summary>
        /// <value>An <see cref="EffectiveAddress"/>.</value>
        public EffectiveAddress Destination
        {
            get
            {
                #region Contract
                Contract.Ensures(Contract.Result<EffectiveAddress>() != null);
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
            // MOVNTI mem32, reg32
            new X86OpcodeVariant(
                new byte[] { 0x0F, 0xC3 },
                new OperandDescriptor(OperandType.MemoryOperand, DataSize.Bit32),
                new OperandDescriptor(OperandType.RegisterOperand, RegisterType.GeneralPurpose32Bit)),
            // MOVNTI mem64, reg64
            new X86OpcodeVariant(
                new byte[] { 0x0F, 0xC3 },
                new OperandDescriptor(OperandType.MemoryOperand, DataSize.Bit64),
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
