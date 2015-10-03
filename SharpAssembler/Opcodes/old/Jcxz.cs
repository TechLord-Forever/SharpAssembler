
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using SharpAssembler;
using SharpAssembler.Architectures.X86.Operands;

namespace SharpAssembler.Architectures.X86.Instructions
{
    /// <summary>
    /// The JCXZ (Jump if CX Zero) instruction.
    /// </summary>
    public class Jcxz : X86Instruction
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Jcxz"/> class.
        /// </summary>
        /// <param name="target">The target.</param>
        public Jcxz(RelativeOffset target)
            : this(target, DataSize.None)
        {
            #region Contract
            Contract.Requires<ArgumentNullException>(target != null);
            #endregion
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Jcxz"/> class.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="operandSize">The operand size.</param>
        public Jcxz(RelativeOffset target, DataSize operandSize)
        {
            #region Contract
            Contract.Requires<ArgumentNullException>(target != null);
            Contract.Requires<InvalidEnumArgumentException>(Enum.IsDefined(typeof(DataSize), operandSize));
            Contract.Requires<ArgumentException>(operandSize == DataSize.None || operandSize == DataSize.Bit16 ||
                operandSize == DataSize.Bit32 || operandSize == DataSize.Bit64,
                "The size must be either None, 16, 32 or 64-bits.");
            #endregion

            this.target = target;
            this.OperandSize = operandSize;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the mnemonic of the instruction.
        /// </summary>
        /// <value>The mnemonic of the instruction.</value>
        public override string Mnemonic
        {
            get { return "jcxz"; }
        }

        private RelativeOffset target;
        /// <summary>
        /// Gets the target.
        /// </summary>
        /// <value>A <see cref="RelativeOffset"/>.</value>
        public RelativeOffset Target
        {
            get
            {
                #region Contract
                Contract.Ensures(Contract.Result<RelativeOffset>() != null);
                #endregion
                return target;
            }
#if OPERAND_SET
            set
            {
                #region Contract
                Contract.Requires<ArgumentNullException>(value != null);
                #endregion
                target = value;
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
            yield return this.target;
        }
        #endregion

        #region Instruction Variants
        /// <summary>
        /// An array of <see cref="X86OpcodeVariant"/> objects
        /// describing the possible variants of this instruction.
        /// </summary>
        private static X86OpcodeVariant[] variants = new []{
            // JCXZ rel8off
            new X86OpcodeVariant(
                new byte[] { 0xE3 }, 0,
                DataSize.Bit16,
                new OperandDescriptor(OperandType.RelativeOffset, DataSize.Bit8)),
            // JCXZ rel8off
            new X86OpcodeVariant(
                new byte[] { 0xE3 }, 0,
                DataSize.Bit32,
                new OperandDescriptor(OperandType.RelativeOffset, DataSize.Bit8)),
            // JCXZ rel8off
            new X86OpcodeVariant(
                new byte[] { 0xE3 }, 0,
                DataSize.Bit64,
                new OperandDescriptor(OperandType.RelativeOffset, DataSize.Bit8)),
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



