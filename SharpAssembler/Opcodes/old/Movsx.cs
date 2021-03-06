﻿
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using SharpAssembler.Architectures.X86.Operands;

namespace SharpAssembler.Architectures.X86.Instructions
{
    /// <summary>
    /// The MOVSX (Move with Sign-Extension) instruction.
    /// </summary>
    public class Movsx : X86Instruction
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Movsx"/> class.
        /// </summary>
        /// <param name="destination">The destination register.</param>
        /// <param name="source">The source register.</param>
        public Movsx(RegisterOperand destination, RegisterOperand source)
            : this(destination, (Operand)source)
        {
            #region Contract
            Contract.Requires<ArgumentNullException>(destination != null);
            Contract.Requires<ArgumentNullException>(source != null);
            #endregion
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Movsx"/> class.
        /// </summary>
        /// <param name="destination">The destination register.</param>
        /// <param name="source">The source memory operand.</param>
        public Movsx(RegisterOperand destination, EffectiveAddress source)
            : this(destination, (Operand)source)
        {
            #region Contract
            Contract.Requires<ArgumentNullException>(destination != null);
            Contract.Requires<ArgumentNullException>(source != null);
            #endregion
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Movsx"/> class.
        /// </summary>
        /// <param name="destination">The destination operand.</param>
        /// <param name="source">The source operand.</param>
        private Movsx(RegisterOperand destination, Operand source)
        {
            #region Contract
            Contract.Requires<ArgumentNullException>(destination != null);
            Contract.Requires<ArgumentNullException>(source != null);
            Contract.Requires<InvalidCastException>(
                    source is EffectiveAddress ||
                    source is RegisterOperand);
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
            get { return "movsx"; }
        }

        private Operand source;
        /// <summary>
        /// Gets the source operand of the instruction.
        /// </summary>
        /// <value>An <see cref="Operand"/>.</value>
        public Operand Source
        {
            get
            {
                #region Contract
                Contract.Ensures(Contract.Result<Operand>() != null);
                Contract.Ensures(
                    Contract.Result<Operand>() is EffectiveAddress ||
                    Contract.Result<Operand>() is RegisterOperand);
                #endregion
                return source;
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
                return destination;
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
            // MOVSX reg16, reg/mem8
            new X86OpcodeVariant(
                new byte[] { 0x0F, 0xBE },
                new OperandDescriptor(OperandType.RegisterOperand, RegisterType.GeneralPurpose16Bit),
                new OperandDescriptor(OperandType.RegisterOrMemoryOperand, RegisterType.GeneralPurpose8Bit)),
            // MOVSX reg32, reg/mem8
            new X86OpcodeVariant(
                new byte[] { 0x0F, 0xBE },
                new OperandDescriptor(OperandType.RegisterOperand, RegisterType.GeneralPurpose32Bit),
                new OperandDescriptor(OperandType.RegisterOrMemoryOperand, RegisterType.GeneralPurpose8Bit)),
            // MOVSX reg64, reg/mem8
            new X86OpcodeVariant(
                new byte[] { 0x0F, 0xBE },
                new OperandDescriptor(OperandType.RegisterOperand, RegisterType.GeneralPurpose64Bit),
                new OperandDescriptor(OperandType.RegisterOrMemoryOperand, RegisterType.GeneralPurpose8Bit)),

            // MOVSX reg32, reg/mem16
            new X86OpcodeVariant(
                new byte[] { 0x0F, 0xBF },
                new OperandDescriptor(OperandType.RegisterOperand, RegisterType.GeneralPurpose32Bit),
                new OperandDescriptor(OperandType.RegisterOrMemoryOperand, RegisterType.GeneralPurpose16Bit)),
            // MOVSX reg64, reg/mem16
            new X86OpcodeVariant(
                new byte[] { 0x0F, 0xBF },
                new OperandDescriptor(OperandType.RegisterOperand, RegisterType.GeneralPurpose64Bit),
                new OperandDescriptor(OperandType.RegisterOrMemoryOperand, RegisterType.GeneralPurpose16Bit)),
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
