﻿
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using SharpAssembler;
using SharpAssembler.Architectures.X86.Operands;

namespace SharpAssembler.Architectures.X86.Instructions
{
    /// <summary>
    /// The SAR (Shift Arithmetic Right) instruction.
    /// </summary>
    public class Sar : X86Instruction
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Sar"/> class.
        /// </summary>
        /// <param name="value">The value to change.</param>
        public Sar(EffectiveAddress value)
            : this((Operand)value, null)
        {
            #region Contract
            Contract.Requires<ArgumentNullException>(value != null);
            #endregion
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Sar"/> class.
        /// </summary>
        /// <param name="value">The value to change.</param>
        public Sar(RegisterOperand value)
            : this((Operand)value, null)
        {
            #region Contract
            Contract.Requires<ArgumentNullException>(value != null);
            #endregion
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Sar"/> class.
        /// </summary>
        /// <param name="value">The value to change.</param>
        /// <param name="positions">The register containing the number of positions to adjust.</param>
        public Sar(EffectiveAddress value, RegisterOperand positions)
            : this((Operand)value, (Operand)positions)
        {
            #region Contract
            Contract.Requires<ArgumentNullException>(value != null);
            Contract.Requires<ArgumentNullException>(positions != null);
            #endregion
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Sar"/> class.
        /// </summary>
        /// <param name="value">The value to change.</param>
        /// <param name="positions">The register containing the number of positions to adjust.</param>
        public Sar(RegisterOperand value, RegisterOperand positions)
            : this((Operand)value, (Operand)positions)
        {
            #region Contract
            Contract.Requires<ArgumentNullException>(value != null);
            Contract.Requires<ArgumentNullException>(positions != null);
            #endregion
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Sar"/> class.
        /// </summary>
        /// <param name="value">The value to change.</param>
        /// <param name="positions">The number of positions to adjust.</param>
        public Sar(EffectiveAddress value, Immediate positions)
            : this((Operand)value, (Operand)positions)
        {
            #region Contract
            Contract.Requires<ArgumentNullException>(value != null);
            Contract.Requires<ArgumentNullException>(positions != null);
            #endregion
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Sar"/> class.
        /// </summary>
        /// <param name="value">The value to change.</param>
        /// <param name="positions">The number of positions to adjust.</param>
        public Sar(RegisterOperand value, Immediate positions)
            : this((Operand)value, (Operand)positions)
        {
            #region Contract
            Contract.Requires<ArgumentNullException>(value != null);
            Contract.Requires<ArgumentNullException>(positions != null);
            #endregion
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Sar"/> class.
        /// </summary>
        /// <param name="value">The value to change.</param>
        /// <param name="positions">The number of positions to adjust;
        /// or <see langword="null"/> to adjust one position.</param>
        private Sar(Operand value, Operand positions)
        {
            #region Contract
            Contract.Requires<ArgumentNullException>(value != null);
            Contract.Requires<InvalidCastException>(
                    value is EffectiveAddress ||
                    value is RegisterOperand);
            Contract.Requires<InvalidCastException>(positions != null || (
                    positions is Immediate ||
                    positions is EffectiveAddress ||
                    positions is RegisterOperand));
            #endregion

            this.value = value;
            this.positions = positions;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the mnemonic of the instruction.
        /// </summary>
        /// <value>The mnemonic of the instruction.</value>
        public override string Mnemonic
        {
            get { return "sar"; }
        }

        private Operand positions;
        /// <summary>
        /// Gets the number of positions to shift.
        /// </summary>
        /// <value>An <see cref="Operand"/>; or <see langword="null"/>.</value>
        public Operand Positions
        {
            get
            {
                #region Contract
                Contract.Ensures(Contract.Result<Operand>() == null || (
                    Contract.Result<Operand>() is Immediate ||
                    Contract.Result<Operand>() is EffectiveAddress ||
                    Contract.Result<Operand>() is RegisterOperand));
                #endregion
                return positions;
            }
#if OPERAND_SET
            set
            {
                #region Contract
                Contract.Requires<InvalidCastException>(value == null || (
                    value is Immediate ||
                    value is EffectiveAddress ||
                    value is RegisterOperand));
                #endregion
                positions = value;
            }
#endif
        }

        private Operand value;
        /// <summary>
        /// Gets the value being modified.
        /// </summary>
        /// <value>An <see cref="Operand"/>.</value>
        public Operand Value
        {
            get
            {
                #region Contract
                Contract.Ensures(Contract.Result<Operand>() != null);
                Contract.Ensures(
                    Contract.Result<Operand>() is EffectiveAddress ||
                    Contract.Result<Operand>() is RegisterOperand);
                #endregion
                return value;
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
                this.value = value;
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
            yield return this.value;
            yield return this.positions;
        }
        #endregion

        #region Instruction Variants
        /// <summary>
        /// An array of <see cref="X86OpcodeVariant"/> objects
        /// describing the possible variants of this instruction.
        /// </summary>
        private static X86OpcodeVariant[] variants = new[]{
            // SAR reg/mem8, 1
            new X86OpcodeVariant(
                new byte[] { 0xD0 }, 7,
                new OperandDescriptor(OperandType.RegisterOrMemoryOperand, DataSize.Bit8),
                new OperandDescriptor(OperandType.None, DataSize.None)),
            // SAR reg/mem8, CL
            new X86OpcodeVariant(
                new byte[] { 0xD2 }, 7,
                new OperandDescriptor(OperandType.RegisterOrMemoryOperand, DataSize.Bit8),
                new OperandDescriptor(Register.CL)),
            // SAR reg/mem8, imm8
            new X86OpcodeVariant(
                new byte[] { 0xC0 }, 7,
                new OperandDescriptor(OperandType.RegisterOrMemoryOperand, DataSize.Bit8),
                new OperandDescriptor(OperandType.Immediate, DataSize.Bit8)),



            // SAR reg/mem16, 1
            new X86OpcodeVariant(
                new byte[] { 0xD1 }, 7,
                new OperandDescriptor(OperandType.RegisterOrMemoryOperand, DataSize.Bit8),
                new OperandDescriptor(OperandType.None, DataSize.None)),
            // SAR reg/mem16, CL
            new X86OpcodeVariant(
                new byte[] { 0xD3 }, 7,
                new OperandDescriptor(OperandType.RegisterOrMemoryOperand, DataSize.Bit8),
                new OperandDescriptor(Register.CL)),
            // SAR reg/mem16, imm8
            new X86OpcodeVariant(
                new byte[] { 0xC1 }, 7,
                new OperandDescriptor(OperandType.RegisterOrMemoryOperand, DataSize.Bit8),
                new OperandDescriptor(OperandType.Immediate, DataSize.Bit8)),



            // SAR reg/mem32, 1
            new X86OpcodeVariant(
                new byte[] { 0xD1 }, 7,
                new OperandDescriptor(OperandType.RegisterOrMemoryOperand, DataSize.Bit8),
                new OperandDescriptor(OperandType.None, DataSize.None)),
            // SAR reg/mem32, CL
            new X86OpcodeVariant(
                new byte[] { 0xD3 }, 7,
                new OperandDescriptor(OperandType.RegisterOrMemoryOperand, DataSize.Bit8),
                new OperandDescriptor(Register.CL)),
            // SAR reg/mem32, imm8
            new X86OpcodeVariant(
                new byte[] { 0xC1 }, 7,
                new OperandDescriptor(OperandType.RegisterOrMemoryOperand, DataSize.Bit8),
                new OperandDescriptor(OperandType.Immediate, DataSize.Bit8)),



            // SAR reg/mem64, 1
            new X86OpcodeVariant(
                new byte[] { 0xD1 }, 7,
                new OperandDescriptor(OperandType.RegisterOrMemoryOperand, DataSize.Bit8),
                new OperandDescriptor(OperandType.None, DataSize.None)),
            // SAR reg/mem64, CL
            new X86OpcodeVariant(
                new byte[] { 0xD3 }, 7,
                new OperandDescriptor(OperandType.RegisterOrMemoryOperand, DataSize.Bit8),
                new OperandDescriptor(Register.CL)),
            // SAR reg/mem64, imm8
            new X86OpcodeVariant(
                new byte[] { 0xC1 }, 7,
                new OperandDescriptor(OperandType.RegisterOrMemoryOperand, DataSize.Bit8),
                new OperandDescriptor(OperandType.Immediate, DataSize.Bit8)),
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
