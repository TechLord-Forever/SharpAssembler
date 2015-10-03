
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using SharpAssembler;
using SharpAssembler.Architectures.X86.Operands;

namespace SharpAssembler.Architectures.X86.Instructions
{
    /// <summary>
    /// The CALL (Procedure Call) instruction.
    /// </summary>
    public class Call : X86Instruction
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Call"/> class.
        /// </summary>
        /// <param name="target">The target <see cref="RelativeOffset"/>.</param>
        public Call(RelativeOffset target)
            : this((Operand)target)
        {
            #region Contract
            Contract.Requires<ArgumentNullException>(target != null);
            #endregion
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Call"/> class.
        /// </summary>
        /// <param name="target">The <see cref="RegisterOperand"/> containing the target's address.</param>
        public Call(RegisterOperand target)
            : this((Operand)target)
        {
            #region Contract
            Contract.Requires<ArgumentNullException>(target != null);
            #endregion
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Call"/> class.
        /// </summary>
        /// <param name="target">The <see cref="EffectiveAddress"/> containing the target's address.</param>
        public Call(EffectiveAddress target)
            : this((Operand)target)
        {
            #region Contract
            Contract.Requires<ArgumentNullException>(target != null);
            #endregion
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Call"/> class.
        /// </summary>
        /// <param name="target">The target <see cref="FarPointer"/>.</param>
        public Call(FarPointer target)
            : this((Operand)target)
        {
            #region Contract
            Contract.Requires<ArgumentNullException>(target != null);
            #endregion
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Call"/> class.
        /// </summary>
        /// <param name="target">The target <see cref="RelativeOffset"/>, or the address specified at the
        /// <see cref="RegisterOperand"/> or <see cref="EffectiveAddress"/>.</param>
        private Call(Operand target)
        {
            #region Contract
            Contract.Requires<ArgumentNullException>(target != null);
            Contract.Requires<InvalidCastException>(
                target is RegisterOperand ||
                target is EffectiveAddress ||
                target is RelativeOffset ||
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
            get { return "call"; }
        }

        private Operand target;
        /// <summary>
        /// Gets the register or memory operand from which the bit is copied and set.
        /// </summary>
        /// <value>An <see cref="Operand"/>.</value>
        public Operand Target
        {
            get
            {
                #region Contract
                Contract.Ensures(Contract.Result<Operand>() != null);
                Contract.Ensures(
                    Contract.Result<Operand>() is RegisterOperand ||
                    Contract.Result<Operand>() is EffectiveAddress ||
                    Contract.Result<Operand>() is RelativeOffset ||
                    Contract.Result<Operand>() is FarPointer);
                #endregion
                return target;
            }
#if OPERAND_SET
            set
            {
                #region Contract
                Contract.Requires<ArgumentNullException>(value != null);
                Contract.Requires<InvalidCastException>(
                    value is RegisterOperand ||
                    value is EffectiveAddress ||
                    value is RelativeOffset ||
                    value is FarPointer);
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
            // CALL rel16off
            new X86OpcodeVariant(
                new byte[] { 0xE8 },
                new OperandDescriptor(OperandType.RelativeOffset, DataSize.Bit16)),
            // CALL rel32off
            new X86OpcodeVariant(
                new byte[] { 0xE8 },
                new OperandDescriptor(OperandType.RelativeOffset, DataSize.Bit32)),

            // CALL reg/mem16
            new X86OpcodeVariant(
                new byte[] { 0xFF }, 2,
                new OperandDescriptor(OperandType.RegisterOrMemoryOperand, RegisterType.GeneralPurpose16Bit)),
            // CALL reg/mem32
            new X86OpcodeVariant(
                new byte[] { 0xFF }, 2,
                new OperandDescriptor(OperandType.RegisterOrMemoryOperand, RegisterType.GeneralPurpose32Bit)),
            // CALL reg/mem64
            new X86OpcodeVariant(
                new byte[] { 0xFF }, 2,
                new OperandDescriptor(OperandType.RegisterOrMemoryOperand, RegisterType.GeneralPurpose64Bit)),

            // CALL pntr16:16
            new X86OpcodeVariant(
                new byte[] { 0x9A },
                new OperandDescriptor(OperandType.FarPointer, DataSize.Bit16)),
            // CALL pntr16:32
            new X86OpcodeVariant(
                new byte[] { 0x9A },
                new OperandDescriptor(OperandType.FarPointer, DataSize.Bit32)),

            // CALL mem16:16
            new X86OpcodeVariant(
                new byte[] { 0xFF }, 3,
                new OperandDescriptor(OperandType.MemoryOperand, DataSize.Bit16)),
            // CALL mem16:32
            new X86OpcodeVariant(
                new byte[] { 0xFF }, 3,
                new OperandDescriptor(OperandType.MemoryOperand, DataSize.Bit32)),
            // CALL mem16:64
            new X86OpcodeVariant(
                new byte[] { 0xFF }, 3,
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