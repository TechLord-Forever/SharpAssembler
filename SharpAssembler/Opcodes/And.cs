using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using SharpAssembler;
using SharpAssembler.Architectures.X86.Operands;

namespace SharpAssembler.Architectures.X86.Instructions
{
    /// <summary>
    /// The AND (Logical AND) instruction.
    /// </summary>
    public class And : X86Instruction, ILockInstruction
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="And"/> class.
        /// </summary>
        /// <param name="destination">The destination register operand.</param>
        /// <param name="source">The source immediate value.</param>
        public And(RegisterOperand destination, Immediate source)
            : this((Operand)destination, (Operand)source)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="And"/> class.
        /// </summary>
        /// <param name="destination">The destination memory operand.</param>
        /// <param name="source">The source immediate value.</param>
        public And(EffectiveAddress destination, Immediate source)
            : this((Operand)destination, (Operand)source)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="And"/> class.
        /// </summary>
        /// <param name="destination">The destination register operand.</param>
        /// <param name="source">The source immediate register operand.</param>
        public And(RegisterOperand destination, RegisterOperand source)
            : this((Operand)destination, (Operand)source)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="And"/> class.
        /// </summary>
        /// <param name="destination">The destination memory operand.</param>
        /// <param name="source">The source immediate register operand.</param>
        public And(EffectiveAddress destination, RegisterOperand source)
            : this((Operand)destination, (Operand)source)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="And"/> class.
        /// </summary>
        /// <param name="destination">The destination register operand.</param>
        /// <param name="source">The source immediate memory operand.</param>
        public And(RegisterOperand destination, EffectiveAddress source)
            : this((Operand)destination, (Operand)source)
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="And"/> class.
        /// </summary>
        /// <param name="destination">The destination operand.</param>
        /// <param name="source">The source operand.</param>
        private And(Operand destination, Operand source)
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
            get { return "and"; }
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
        private static X86OpcodeVariant[] variants = new[]
        {
            // AND AL, imm8
            new X86OpcodeVariant(
                new byte[] { 0x24 },
                new OperandDescriptor(Register.AL),
                new OperandDescriptor(OperandType.Immediate, DataSize.Bit8)),
            // AND AX, imm16
            new X86OpcodeVariant(
                new byte[] { 0x25 },
                new OperandDescriptor(Register.AX),
                new OperandDescriptor(OperandType.Immediate, DataSize.Bit16)),
            // AND EAX, imm32
            new X86OpcodeVariant(
                new byte[] { 0x25 },
                new OperandDescriptor(Register.EAX),
                new OperandDescriptor(OperandType.Immediate, DataSize.Bit32)),
            // AND RAX, imm32
            new X86OpcodeVariant(
                new byte[] { 0x25 },
                new OperandDescriptor(Register.RAX),
                new OperandDescriptor(OperandType.Immediate, DataSize.Bit32)),

            // AND reg/mem8, imm8
            new X86OpcodeVariant(
                new byte[] { 0x80 }, 4,
                new OperandDescriptor(OperandType.RegisterOrMemoryOperand, RegisterType.GeneralPurpose8Bit),
                new OperandDescriptor(OperandType.Immediate, DataSize.Bit8)),
            // AND reg/mem16, imm16
            new X86OpcodeVariant(
                new byte[] { 0x81 }, 4,
                new OperandDescriptor(OperandType.RegisterOrMemoryOperand, RegisterType.GeneralPurpose16Bit),
                new OperandDescriptor(OperandType.Immediate, DataSize.Bit16)),
            // AND reg/mem32, imm32
            new X86OpcodeVariant(
                new byte[] { 0x81 }, 4,
                new OperandDescriptor(OperandType.RegisterOrMemoryOperand, RegisterType.GeneralPurpose32Bit),
                new OperandDescriptor(OperandType.Immediate, DataSize.Bit32)),
            // AND reg/mem64, imm32
            new X86OpcodeVariant(
                new byte[] { 0x81 }, 4,
                new OperandDescriptor(OperandType.RegisterOrMemoryOperand, RegisterType.GeneralPurpose64Bit),
                new OperandDescriptor(OperandType.Immediate, DataSize.Bit32)),

            // AND reg/mem16, imm8
            new X86OpcodeVariant(
                new byte[] { 0x83 }, 4,
                new OperandDescriptor(OperandType.RegisterOrMemoryOperand, RegisterType.GeneralPurpose16Bit),
                new OperandDescriptor(OperandType.Immediate, DataSize.Bit8)),
            // AND reg/mem32, imm8
            new X86OpcodeVariant(
                new byte[] { 0x83 }, 4,
                new OperandDescriptor(OperandType.RegisterOrMemoryOperand, RegisterType.GeneralPurpose32Bit),
                new OperandDescriptor(OperandType.Immediate, DataSize.Bit8)),
            // AND reg/mem64, imm8
            new X86OpcodeVariant(
                new byte[] { 0x83 }, 4,
                new OperandDescriptor(OperandType.RegisterOrMemoryOperand, RegisterType.GeneralPurpose64Bit),
                new OperandDescriptor(OperandType.Immediate, DataSize.Bit8)),


            // AND reg/mem8, reg8
            new X86OpcodeVariant(
                new byte[] { 0x20 },
                new OperandDescriptor(OperandType.RegisterOrMemoryOperand, RegisterType.GeneralPurpose8Bit),
                new OperandDescriptor(OperandType.RegisterOperand, RegisterType.GeneralPurpose8Bit)),
            // AND reg/mem16, reg16
            new X86OpcodeVariant(
                new byte[] { 0x21 },
                new OperandDescriptor(OperandType.RegisterOrMemoryOperand, RegisterType.GeneralPurpose16Bit),
                new OperandDescriptor(OperandType.RegisterOperand, RegisterType.GeneralPurpose16Bit)),
            // AND reg/mem32, reg32
            new X86OpcodeVariant(
                new byte[] { 0x21 },
                new OperandDescriptor(OperandType.RegisterOrMemoryOperand, RegisterType.GeneralPurpose32Bit),
                new OperandDescriptor(OperandType.RegisterOperand, RegisterType.GeneralPurpose32Bit)),
            // AND reg/mem64, reg64
            new X86OpcodeVariant(
                new byte[] { 0x21 },
                new OperandDescriptor(OperandType.RegisterOrMemoryOperand, RegisterType.GeneralPurpose64Bit),
                new OperandDescriptor(OperandType.RegisterOperand, RegisterType.GeneralPurpose64Bit)),


            // AND reg8, reg/mem8
            new X86OpcodeVariant(
                new byte[] { 0x22 },
                new OperandDescriptor(OperandType.RegisterOperand, RegisterType.GeneralPurpose8Bit),
                new OperandDescriptor(OperandType.RegisterOrMemoryOperand, RegisterType.GeneralPurpose8Bit)),
            // AND reg16, reg/mem16
            new X86OpcodeVariant(
                new byte[] { 0x23 },
                new OperandDescriptor(OperandType.RegisterOperand, RegisterType.GeneralPurpose16Bit),
                new OperandDescriptor(OperandType.RegisterOrMemoryOperand, RegisterType.GeneralPurpose16Bit)),
            // AND reg32, reg/mem32
            new X86OpcodeVariant(
                new byte[] { 0x23 },
                new OperandDescriptor(OperandType.RegisterOperand, RegisterType.GeneralPurpose32Bit),
                new OperandDescriptor(OperandType.RegisterOrMemoryOperand, RegisterType.GeneralPurpose32Bit)),
            // AND reg64, reg/mem64
            new X86OpcodeVariant(
                new byte[] { 0x23 },
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
