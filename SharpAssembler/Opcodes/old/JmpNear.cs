
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using SharpAssembler;
using SharpAssembler.Architectures.X86.Operands;

namespace SharpAssembler.Architectures.X86.Instructions
{
    /// <summary>
    /// The JMP NEAR (Jump Near) instruction.
    /// </summary>
    public class JmpNear : X86Instruction
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="JmpNear"/> class.
        /// </summary>
        /// <param name="target">The relative offset to the jump target.</param>
        public JmpNear(RelativeOffset target)
            : this((Operand)target)
        {
            #region Contract
            Contract.Requires<ArgumentNullException>(target != null);
            #endregion
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="JmpNear"/> class.
        /// </summary>
        /// <param name="target">The register containing the new near jump target address.</param>
        public JmpNear(RegisterOperand target)
            : this((Operand)target)
        {
            #region Contract
            Contract.Requires<ArgumentNullException>(target != null);
            #endregion
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="JmpNear"/> class.
        /// </summary>
        /// <param name="target">The memory location containing the new near jump target address.</param>
        public JmpNear(EffectiveAddress target)
            : this((Operand)target)
        {
            #region Contract
            Contract.Requires<ArgumentNullException>(target != null);
            #endregion
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="JmpNear"/> class.
        /// </summary>
        /// <param name="target">The operand.</param>
        private JmpNear(Operand target)
        {
            #region Contract
            Contract.Requires<ArgumentNullException>(target != null);
            Contract.Requires<InvalidCastException>(
                    target is EffectiveAddress ||
                    target is RegisterOperand ||
                    target is RelativeOffset);
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
                    Contract.Result<Operand>() is RelativeOffset ||
                    Contract.Result<Operand>() is EffectiveAddress ||
                    Contract.Result<Operand>() is RegisterOperand);
                #endregion
                return target;
            }
#if OPERAND_SET
            set
            {
                #region Contract
                Contract.Requires<ArgumentNullException>(value != null);
                Contract.Requires<InvalidCastException>(
                    value is RelativeOffset ||
                    value is EffectiveAddress ||
                    value is RegisterOperand);
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
            // JMP rel8off
            new X86OpcodeVariant(
                new byte[] { 0xEB },
                new OperandDescriptor(OperandType.RelativeOffset, DataSize.Bit8)),
            // JMP rel16off
            new X86OpcodeVariant(
                new byte[] { 0xE9 },
                new OperandDescriptor(OperandType.RelativeOffset, DataSize.Bit16)),
            // JMP rel32off
            new X86OpcodeVariant(
                new byte[] { 0xE9 },
                new OperandDescriptor(OperandType.RelativeOffset, DataSize.Bit32)),

            // JMP reg/mem16
            new X86OpcodeVariant(
                new byte[] { 0xFF }, 4,
                new OperandDescriptor(OperandType.RegisterOrMemoryOperand, RegisterType.GeneralPurpose16Bit)),
            // JMP reg/mem32
            new X86OpcodeVariant(
                new byte[] { 0xFF }, 4,
                new OperandDescriptor(OperandType.RegisterOrMemoryOperand, RegisterType.GeneralPurpose32Bit)),
            // JMP reg/mem64
            new X86OpcodeVariant(
                new byte[] { 0xFF }, 4,
                new OperandDescriptor(OperandType.RegisterOrMemoryOperand, RegisterType.GeneralPurpose64Bit)),
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
