
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using SharpAssembler;
using SharpAssembler.Architectures.X86.Operands;

namespace SharpAssembler.Architectures.X86.Instructions
{
    /// <summary>
    /// The JMP FAR (Jump Far) instruction.
    /// </summary>
    public class JmpFar : X86Instruction
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="JmpFar"/> class.
        /// </summary>
        /// <param name="target">A far pointer with the new far jump target address and segment.</param>
        public JmpFar(FarPointer target)
            : this((Operand)target)
        {
            #region Contract
            Contract.Requires<ArgumentNullException>(target != null);
            #endregion
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="JmpFar"/> class.
        /// </summary>
        /// <param name="target">The memory location containing the new far jump target address and segment.</param>
        public JmpFar(EffectiveAddress target)
            : this((Operand)target)
        {
            #region Contract
            Contract.Requires<ArgumentNullException>(target != null);
            #endregion
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="JmpFar"/> class.
        /// </summary>
        /// <param name="target">The operand.</param>
        public JmpFar(Operand target)
        {
            #region Contract
            Contract.Requires<ArgumentNullException>(target != null);
            Contract.Requires<InvalidCastException>(
                    target is EffectiveAddress ||
                    target is FarPointer);
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
            get { return "jmp"; }
        }

        private Operand target;
        /// <summary>
        /// Gets the target of the instruction.
        /// </summary>
        /// <value>An <see cref="Operand"/>.</value>
        public Operand Target
        {
            get
            {
                #region Contract
                Contract.Ensures(Contract.Result<Operand>() != null);
                Contract.Ensures(
                    Contract.Result<Operand>() is FarPointer ||
                    Contract.Result<Operand>() is EffectiveAddress);
                #endregion
                return target;
            }
#if OPERAND_SET
            set
            {
                #region Contract
                Contract.Requires<ArgumentNullException>(value != null);
                Contract.Requires<InvalidCastException>(
                    value is FarPointer ||
                    value is EffectiveAddress);
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
            // JMP pntr16:16
            new X86OpcodeVariant(
                new byte[] { 0xEA },
                new OperandDescriptor(OperandType.FarPointer, DataSize.Bit16)),
            // JMP pntr16:32
            new X86OpcodeVariant(
                new byte[] { 0xEA },
                new OperandDescriptor(OperandType.FarPointer, DataSize.Bit32)),

            // JMP mem16:16
            new X86OpcodeVariant(
                new byte[] { 0xFF }, 5,
                new OperandDescriptor(OperandType.MemoryOperand, DataSize.Bit16)),
            // JMP mem16:32
            new X86OpcodeVariant(
                new byte[] { 0xFF }, 5,
                new OperandDescriptor(OperandType.MemoryOperand, DataSize.Bit32)),
            // JMP mem16:64
            new X86OpcodeVariant(
                new byte[] { 0xFF }, 5,
                new OperandDescriptor(OperandType.MemoryOperand, DataSize.Bit64)),
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
