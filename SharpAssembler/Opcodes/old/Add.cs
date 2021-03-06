﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using SharpAssembler;
using SharpAssembler.Architectures.X86.Operands;

namespace SharpAssembler.Architectures.X86.Instructions
{
    /// <summary>
    /// The ADD (Signed or Unsigned Add) instruction.
    /// </summary>
    public class Add : X86Instruction, ILockInstruction
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Add"/> class.
        /// </summary>
        /// <param name="destination">The destination register operand.</param>
        /// <param name="source">The source immediate value.</param>
        public Add(RegisterOperand destination, Immediate source)
            : this((Operand)destination, (Operand)source)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Add"/> class.
        /// </summary>
        /// <param name="destination">The destination memory operand.</param>
        /// <param name="source">The source immediate value.</param>
        public Add(EffectiveAddress destination, Immediate source)
            : this((Operand)destination, (Operand)source)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Add"/> class.
        /// </summary>
        /// <param name="destination">The destination register operand.</param>
        /// <param name="source">The source immediate register operand.</param>
        public Add(RegisterOperand destination, RegisterOperand source)
            : this((Operand)destination, (Operand)source)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Add"/> class.
        /// </summary>
        /// <param name="destination">The destination memory operand.</param>
        /// <param name="source">The source immediate register operand.</param>
        public Add(EffectiveAddress destination, RegisterOperand source)
            : this((Operand)destination, (Operand)source)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Add"/> class.
        /// </summary>
        /// <param name="destination">The destination register operand.</param>
        /// <param name="source">The source immediate memory operand.</param>
        public Add(RegisterOperand destination, EffectiveAddress source)
            : this((Operand)destination, (Operand)source)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Add"/> class.
        /// </summary>
        /// <param name="destination">The destination operand.</param>
        /// <param name="source">The source operand.</param>
        private Add(Operand destination, Operand source)
        {
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
            get { return "add"; }
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
                return source;
            }
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
                return destination;
            }
        }

        private bool lockInstruction = false;
        /// <summary>
        /// Gets or sets whether the lock prefix is used.
        /// </summary>
        /// <value><see langword="true"/> to enable the lock prefix; otherwise, <see langword="false"/>.
        /// The default is <see langword="false"/>.</value>
        /// <remarks>
        /// When this property is set to <see langword="true"/>, the lock signal is asserted before accessing the
        /// specified memory location. When the lock signal has already been asserted, the instruction must wait for it
        /// to be released. Instructions without the lock prefix do not check the lock signal, and will be executed
        /// even when the lock signal is asserted by some other instruction.
        /// </remarks>
        public bool Lock
        {
            get { return lockInstruction; }
            set { lockInstruction = value; }
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
        /// An array of <see cref="X86Instruction.X86OpcodeVariant"/> objects
        /// describing the possible variants of this instruction.
        /// </summary>
        private static X86OpcodeVariant[] variants = new []
        {
            // ADD AL, imm8
            new X86OpcodeVariant(
                new byte[] { 0x04 },
                new OperandDescriptor(Register.AL),
                new OperandDescriptor(OperandType.Immediate, DataSize.Bit8)),
            // ADD AX, imm16
            new X86OpcodeVariant(
                new byte[] { 0x05 },
                new OperandDescriptor(Register.AX),
                new OperandDescriptor(OperandType.Immediate, DataSize.Bit16)),
            // ADD EAX, imm32
            new X86OpcodeVariant(
                new byte[] { 0x05 },
                new OperandDescriptor(Register.EAX),
                new OperandDescriptor(OperandType.Immediate, DataSize.Bit32)),
            // ADD RAX, imm32
            new X86OpcodeVariant(
                new byte[] { 0x05 },
                new OperandDescriptor(Register.RAX),
                new OperandDescriptor(OperandType.Immediate, DataSize.Bit32)),

            // ADD reg/mem8, imm8
            new X86OpcodeVariant(
                new byte[] { 0x80 }, 0,
                new OperandDescriptor(OperandType.RegisterOrMemoryOperand, RegisterType.GeneralPurpose8Bit),
                new OperandDescriptor(OperandType.Immediate, DataSize.Bit8)),
            // ADD reg/mem16, imm16
            new X86OpcodeVariant(
                new byte[] { 0x81 }, 0,
                new OperandDescriptor(OperandType.RegisterOrMemoryOperand, RegisterType.GeneralPurpose16Bit),
                new OperandDescriptor(OperandType.Immediate, DataSize.Bit16)),
            // ADD reg/mem32, imm32
            new X86OpcodeVariant(
                new byte[] { 0x81 }, 0,
                new OperandDescriptor(OperandType.RegisterOrMemoryOperand, RegisterType.GeneralPurpose32Bit),
                new OperandDescriptor(OperandType.Immediate, DataSize.Bit32)),
            // ADD reg/mem64, imm32
            new X86OpcodeVariant(
                new byte[] { 0x81 }, 0,
                new OperandDescriptor(OperandType.RegisterOrMemoryOperand, RegisterType.GeneralPurpose64Bit),
                new OperandDescriptor(OperandType.Immediate, DataSize.Bit32)),

            // ADD reg/mem16, imm8
            new X86OpcodeVariant(
                new byte[] { 0x83 }, 0,
                new OperandDescriptor(OperandType.RegisterOrMemoryOperand, RegisterType.GeneralPurpose16Bit),
                new OperandDescriptor(OperandType.Immediate, DataSize.Bit8)),
            // ADD reg/mem32, imm8
            new X86OpcodeVariant(
                new byte[] { 0x83 }, 0,
                new OperandDescriptor(OperandType.RegisterOrMemoryOperand, RegisterType.GeneralPurpose32Bit),
                new OperandDescriptor(OperandType.Immediate, DataSize.Bit8)),
            // ADD reg/mem64, imm8
            new X86OpcodeVariant(
                new byte[] { 0x83 }, 0,
                new OperandDescriptor(OperandType.RegisterOrMemoryOperand, RegisterType.GeneralPurpose64Bit),
                new OperandDescriptor(OperandType.Immediate, DataSize.Bit8)),


            // ADD reg/mem8, reg8
            new X86OpcodeVariant(
                new byte[] { 0x00 },
                new OperandDescriptor(OperandType.RegisterOrMemoryOperand, RegisterType.GeneralPurpose8Bit),
                new OperandDescriptor(OperandType.RegisterOperand, RegisterType.GeneralPurpose8Bit)),
            // ADD reg/mem16, reg16
            new X86OpcodeVariant(
                new byte[] { 0x01 },
                new OperandDescriptor(OperandType.RegisterOrMemoryOperand, RegisterType.GeneralPurpose16Bit),
                new OperandDescriptor(OperandType.RegisterOperand, RegisterType.GeneralPurpose16Bit)),
            // ADD reg/mem32, reg32
            new X86OpcodeVariant(
                new byte[] { 0x01 },
                new OperandDescriptor(OperandType.RegisterOrMemoryOperand, RegisterType.GeneralPurpose32Bit),
                new OperandDescriptor(OperandType.RegisterOperand, RegisterType.GeneralPurpose32Bit)),
            // ADD reg/mem64, reg64
            new X86OpcodeVariant(
                new byte[] { 0x01 },
                new OperandDescriptor(OperandType.RegisterOrMemoryOperand, RegisterType.GeneralPurpose64Bit),
                new OperandDescriptor(OperandType.RegisterOperand, RegisterType.GeneralPurpose64Bit)),


            // ADD reg8, reg/mem8
            new X86OpcodeVariant(
                new byte[] { 0x02 },
                new OperandDescriptor(OperandType.RegisterOperand, RegisterType.GeneralPurpose8Bit),
                new OperandDescriptor(OperandType.RegisterOrMemoryOperand, RegisterType.GeneralPurpose8Bit)),
            // ADD reg16, reg/mem16
            new X86OpcodeVariant(
                new byte[] { 0x03 },
                new OperandDescriptor(OperandType.RegisterOperand, RegisterType.GeneralPurpose16Bit),
                new OperandDescriptor(OperandType.RegisterOrMemoryOperand, RegisterType.GeneralPurpose16Bit)),
            // ADD reg32, reg/mem32
            new X86OpcodeVariant(
                new byte[] { 0x03 },
                new OperandDescriptor(OperandType.RegisterOperand, RegisterType.GeneralPurpose32Bit),
                new OperandDescriptor(OperandType.RegisterOrMemoryOperand, RegisterType.GeneralPurpose32Bit)),
            // ADD reg64, reg/mem64
            new X86OpcodeVariant(
                new byte[] { 0x03 },
                new OperandDescriptor(OperandType.RegisterOperand, RegisterType.GeneralPurpose64Bit),
                new OperandDescriptor(OperandType.RegisterOrMemoryOperand, RegisterType.GeneralPurpose64Bit)),
        };

        /// <summary>
        /// Returns an array containing the <see cref="X86Instruction.X86OpcodeVariant"/>
        /// objects representing all the possible variants of this instruction.
        /// </summary>
        /// <returns>An array of <see cref="X86Instruction.X86OpcodeVariant"/>
        /// objects.</returns>
        internal override X86OpcodeVariant[] GetVariantList()
        { return variants; }
        #endregion
    }
}
