
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using SharpAssembler;
using SharpAssembler.Architectures.X86.Operands;

namespace SharpAssembler.Architectures.X86.Instructions
{
    /// <summary>
    /// The LOOP (Loop) instruction.
    /// </summary>
    public class Loop : X86Instruction
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Loop"/> class.
        /// </summary>
        /// <param name="target">The loop target.</param>
        public Loop(RelativeOffset target)
        {
            #region Contract
            Contract.Requires<ArgumentNullException>(target != null);
            #endregion

            this.target = target;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the mnemonic of the instruction.
        /// </summary>
        /// <value>The mnemonic of the instruction.</value>
        public override string Mnemonic
        {
            get { return "loop"; }
        }

        private RelativeOffset target;
        /// <summary>
        /// Gets the target of the instruction.
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
        private static X86OpcodeVariant[] variants = new[]{
            // LOOP rel8off
            new X86OpcodeVariant(
                new byte[] { 0xE2 },
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
