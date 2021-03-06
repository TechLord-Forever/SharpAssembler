﻿
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using SharpAssembler.Architectures.X86.Operands;

namespace SharpAssembler.Architectures.X86.Instructions
{
    /// <summary>
    /// The POP (Pop Stack) instruction.
    /// </summary>
    public class Pop : X86Instruction
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Pop"/> class.
        /// </summary>
        /// <param name="destination">The destination memory operand.</param>
        public Pop(RegisterOperand destination)
            : this((Operand)destination)
        {
            #region Contract
            Contract.Requires<ArgumentNullException>(destination != null);
            #endregion
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Pop"/> class.
        /// </summary>
        /// <param name="destination">The destination memory operand.</param>
        public Pop(EffectiveAddress destination)
            : this((Operand)destination)
        {
            #region Contract
            Contract.Requires<ArgumentNullException>(destination != null);
            #endregion
        }

        
        
        /// <summary>
        /// Initializes a new instance of the <see cref="Pop"/> class.
        /// </summary>
        /// <param name="destination">The destination operand.</param>
        private Pop(Operand destination)
        {
            #region Contract
            Contract.Requires<ArgumentNullException>(destination != null);
            Contract.Requires<InvalidCastException>(
                    destination is EffectiveAddress ||
                    destination is RegisterOperand);
            #endregion

            this.destination = destination;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the mnemonic of the instruction.
        /// </summary>
        /// <value>The mnemonic of the instruction.</value>
        public override string Mnemonic
        {
            get { return "pop"; }
        }

        private Operand destination;
        /// <summary>
        /// Gets the destination operand of the instruction.
        /// </summary>
        /// <value>An <see cref="Operand"/>.</value>
        public Operand Destination
        {
            get
            {
                #region Contract
                Contract.Ensures(Contract.Result<Operand>() != null);
                Contract.Ensures(
                    Contract.Result<Operand>() is EffectiveAddress ||
                    Contract.Result<Operand>() is RegisterOperand);
                #endregion
                return destination;
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
                this.destination = value;
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
        }
        #endregion

        #region Instruction Variants
        /// <summary>
        /// An array of <see cref="X86OpcodeVariant"/> objects
        /// describing the possible variants of this instruction.
        /// </summary>
        private static X86OpcodeVariant[] variants = new[]{
            // POP reg/mem16
            new X86OpcodeVariant(
                new byte[] { 0x8F }, 0,
                new OperandDescriptor(OperandType.RegisterOrMemoryOperand, RegisterType.GeneralPurpose16Bit)),
            // POP reg/mem32
            new X86OpcodeVariant(
                new byte[] { 0x8F }, 0,
                new OperandDescriptor(OperandType.RegisterOrMemoryOperand, RegisterType.GeneralPurpose32Bit)),
            // POP reg/mem64
            new X86OpcodeVariant(
                new byte[] { 0x8F }, 0,
                new OperandDescriptor(OperandType.RegisterOrMemoryOperand, RegisterType.GeneralPurpose64Bit)),

            // POP reg16
            new X86OpcodeVariant(
                new byte[] { 0x58 },
                new OperandDescriptor(OperandType.RegisterOperand, RegisterType.GeneralPurpose16Bit, OperandEncoding.OpcodeAdd)),
            // POP reg32
            new X86OpcodeVariant(
                new byte[] { 0x58 },
                new OperandDescriptor(OperandType.RegisterOperand, RegisterType.GeneralPurpose32Bit, OperandEncoding.OpcodeAdd)),
            // POP reg64
            new X86OpcodeVariant(
                new byte[] { 0x58 },
                new OperandDescriptor(OperandType.RegisterOperand, RegisterType.GeneralPurpose64Bit, OperandEncoding.OpcodeAdd)),

            // TODO: These three are not valid in 64-bit mode:
            // POP DS
            new X86OpcodeVariant(
                new byte[] { 0x1F },
                new OperandDescriptor(Register.DS)),
            // POP ES
            new X86OpcodeVariant(
                new byte[] { 0x07 },
                new OperandDescriptor(Register.ES)),
            // POP SS
            new X86OpcodeVariant(
                new byte[] { 0x17 },
                new OperandDescriptor(Register.SS)),

            // POP FS
            new X86OpcodeVariant(
                new byte[] { 0x0F, 0xA1 },
                new OperandDescriptor(Register.FS)),
            // POP GS
            new X86OpcodeVariant(
                new byte[] { 0x0F, 0xA9 },
                new OperandDescriptor(Register.GS)),
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
