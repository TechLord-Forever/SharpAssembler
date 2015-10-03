
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using SharpAssembler;
using SharpAssembler.Architectures.X86.Operands;

namespace SharpAssembler.Architectures.X86.Instructions
{
    /// <summary>
    /// The CMP instruction.
    /// </summary>
    public class Cmp : X86Instruction
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Cmp"/> class.
        /// </summary>
        /// <param name="second">The destination register.</param>
        /// <param name="first">The source immediate value.</param>
        public Cmp(RegisterOperand second, Immediate first)
            : this((Operand)second, (Operand)first)
        {
            #region Contract
            Contract.Requires<ArgumentNullException>(second != null);
            Contract.Requires<ArgumentNullException>(first != null);
            #endregion
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Cmp"/> class.
        /// </summary>
        /// <param name="second">The destination memory operand.</param>
        /// <param name="first">The source immediate value.</param>
        public Cmp(EffectiveAddress second, Immediate first)
            : this((Operand)second, (Operand)first)
        {
            #region Contract
            Contract.Requires<ArgumentNullException>(second != null);
            Contract.Requires<ArgumentNullException>(first != null);
            #endregion
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Cmp"/> class.
        /// </summary>
        /// <param name="second">The destination register.</param>
        /// <param name="first">The source register.</param>
        public Cmp(RegisterOperand second, RegisterOperand first)
            : this((Operand)second, (Operand)first)
        {
            #region Contract
            Contract.Requires<ArgumentNullException>(second != null);
            Contract.Requires<ArgumentNullException>(first != null);
            #endregion
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Cmp"/> class.
        /// </summary>
        /// <param name="second">The destination register.</param>
        /// <param name="first">The source memory operand.</param>
        public Cmp(RegisterOperand second, EffectiveAddress first)
            : this((Operand)second, (Operand)first)
        {
            #region Contract
            Contract.Requires<ArgumentNullException>(second != null);
            Contract.Requires<ArgumentNullException>(first != null);
            #endregion
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Cmp"/> class.
        /// </summary>
        /// <param name="second">The destination memory operand.</param>
        /// <param name="first">The source register.</param>
        public Cmp(EffectiveAddress second, RegisterOperand first)
            : this((Operand)second, (Operand)first)
        {
            #region Contract
            Contract.Requires<ArgumentNullException>(second != null);
            Contract.Requires<ArgumentNullException>(first != null);
            #endregion
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Cmp"/> class.
        /// </summary>
        /// <param name="second">The second operand.</param>
        /// <param name="first">The first operand.</param>
        private Cmp(Operand second, Operand first)
        {
            #region Contract
            Contract.Requires<ArgumentNullException>(second != null);
            Contract.Requires<InvalidCastException>(
                    second is EffectiveAddress ||
                    second is RegisterOperand);
            Contract.Requires<ArgumentNullException>(first != null);
            Contract.Requires<InvalidCastException>(
                    first is Immediate ||
                    first is EffectiveAddress ||
                    first is RegisterOperand);
            #endregion

            this.second = second;
            this.first = first;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the mnemonic of the instruction.
        /// </summary>
        /// <value>The mnemonic of the instruction.</value>
        public override string Mnemonic
        {
            get { return "cmp"; }
        }

        private Operand first;
        /// <summary>
        /// Gets the first operand of the instruction.
        /// </summary>
        /// <value>An <see cref="Operand"/>.</value>
        public Operand First
        {
            get
            {
                #region Contract
                Contract.Ensures(Contract.Result<Operand>() != null);
                Contract.Ensures(
                    Contract.Result<Operand>() is Immediate ||
                    Contract.Result<Operand>() is EffectiveAddress ||
                    Contract.Result<Operand>() is RegisterOperand);
                #endregion
                return first;
            }
#if OPERAND_SET
            set
            {
                #region Contract
                Contract.Requires<ArgumentNullException>(value != null);
                Contract.Requires<InvalidCastException>(
                    value is Immediate ||
                    value is EffectiveAddress ||
                    value is RegisterOperand);
                #endregion
                first = value;
            }
#endif
        }

        private Operand second;
        /// <summary>
        /// Gets the second operand of the instruction.
        /// </summary>
        /// <value>An <see cref="Operand"/>.</value>
        public Operand Second
        {
            get
            {
                #region Contract
                Contract.Ensures(Contract.Result<Operand>() != null);
                Contract.Ensures(
                    Contract.Result<Operand>() is EffectiveAddress ||
                    Contract.Result<Operand>() is RegisterOperand);
                #endregion
                return second;
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
                second = value;
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
            yield return this.second;
            yield return this.first;
        }
        #endregion

        #region Instruction Variants
        /// <summary>
        /// An array of <see cref="X86OpcodeVariant"/> objects
        /// describing the possible variants of this instruction.
        /// </summary>
        private static X86OpcodeVariant[] variants = new[]{
            // CMP AL, imm8
            new X86OpcodeVariant(
                new byte[] { 0x3C },
                new OperandDescriptor(Register.AL),
                new OperandDescriptor(OperandType.Immediate, DataSize.Bit8)),
            // CMP AX, imm16
            new X86OpcodeVariant(
                new byte[] { 0x3D },
                new OperandDescriptor(Register.AX),
                new OperandDescriptor(OperandType.Immediate, DataSize.Bit16)),
            // CMP EAX, imm32
            new X86OpcodeVariant(
                new byte[] { 0x3D },
                new OperandDescriptor(Register.EAX),
                new OperandDescriptor(OperandType.Immediate, DataSize.Bit32)),
            // CMP RAX, imm32
            new X86OpcodeVariant(
                new byte[] { 0x3D },
                new OperandDescriptor(Register.RAX),
                new OperandDescriptor(OperandType.Immediate, DataSize.Bit32)),



            // CMP reg/mem8, imm8
            new X86OpcodeVariant(
                new byte[] { 0x80 }, 7,
                new OperandDescriptor(OperandType.RegisterOrMemoryOperand, RegisterType.GeneralPurpose8Bit),
                new OperandDescriptor(OperandType.Immediate, DataSize.Bit8)),
            // CMP reg/mem16, imm16
            new X86OpcodeVariant(
                new byte[] { 0x81 }, 7,
                new OperandDescriptor(OperandType.RegisterOrMemoryOperand, RegisterType.GeneralPurpose16Bit),
                new OperandDescriptor(OperandType.Immediate, DataSize.Bit16)),
            // CMP reg/mem32, imm32
            new X86OpcodeVariant(
                new byte[] { 0x81 }, 7,
                new OperandDescriptor(OperandType.RegisterOrMemoryOperand, RegisterType.GeneralPurpose32Bit),
                new OperandDescriptor(OperandType.Immediate, DataSize.Bit32)),
            // CMP reg/mem64, imm32
            new X86OpcodeVariant(
                new byte[] { 0x81 }, 7,
                new OperandDescriptor(OperandType.RegisterOrMemoryOperand, RegisterType.GeneralPurpose64Bit),
                new OperandDescriptor(OperandType.Immediate, DataSize.Bit32)),

            // CMP reg/mem16, imm8
            new X86OpcodeVariant(
                new byte[] { 0x83 }, 7,
                new OperandDescriptor(OperandType.RegisterOrMemoryOperand, RegisterType.GeneralPurpose16Bit),
                new OperandDescriptor(OperandType.Immediate, DataSize.Bit8)),
            // CMP reg/mem32, imm8
            new X86OpcodeVariant(
                new byte[] { 0x83 }, 7,
                new OperandDescriptor(OperandType.RegisterOrMemoryOperand, RegisterType.GeneralPurpose32Bit),
                new OperandDescriptor(OperandType.Immediate, DataSize.Bit8)),
            // CMP reg/mem64, imm8
            new X86OpcodeVariant(
                new byte[] { 0x83 }, 7,
                new OperandDescriptor(OperandType.RegisterOrMemoryOperand, RegisterType.GeneralPurpose64Bit),
                new OperandDescriptor(OperandType.Immediate, DataSize.Bit8)),


            // CMP reg/mem8, reg8
            new X86OpcodeVariant(
                new byte[] { 0x38 },
                new OperandDescriptor(OperandType.RegisterOrMemoryOperand, RegisterType.GeneralPurpose8Bit),
                new OperandDescriptor(OperandType.RegisterOperand, RegisterType.GeneralPurpose8Bit)),
            // CMP reg/mem16, reg16
            new X86OpcodeVariant(
                new byte[] { 0x39 },
                new OperandDescriptor(OperandType.RegisterOrMemoryOperand, RegisterType.GeneralPurpose16Bit),
                new OperandDescriptor(OperandType.RegisterOperand, RegisterType.GeneralPurpose16Bit)),
            // CMP reg/mem32, reg32
            new X86OpcodeVariant(
                new byte[] { 0x39 },
                new OperandDescriptor(OperandType.RegisterOrMemoryOperand, RegisterType.GeneralPurpose32Bit),
                new OperandDescriptor(OperandType.RegisterOperand, RegisterType.GeneralPurpose32Bit)),
            // CMP reg/mem64, reg64
            new X86OpcodeVariant(
                new byte[] { 0x39 },
                new OperandDescriptor(OperandType.RegisterOrMemoryOperand, RegisterType.GeneralPurpose64Bit),
                new OperandDescriptor(OperandType.RegisterOperand, RegisterType.GeneralPurpose64Bit)),


            // CMP reg8, reg/mem8
            new X86OpcodeVariant(
                new byte[] { 0x3A },
                new OperandDescriptor(OperandType.RegisterOperand, RegisterType.GeneralPurpose8Bit),
                new OperandDescriptor(OperandType.RegisterOrMemoryOperand, RegisterType.GeneralPurpose8Bit)),
            // CMP reg16, reg/mem16
            new X86OpcodeVariant(
                new byte[] { 0x3B },
                new OperandDescriptor(OperandType.RegisterOperand, RegisterType.GeneralPurpose16Bit),
                new OperandDescriptor(OperandType.RegisterOrMemoryOperand, RegisterType.GeneralPurpose16Bit)),
            // CMP reg32, reg/mem32
            new X86OpcodeVariant(
                new byte[] { 0x3B },
                new OperandDescriptor(OperandType.RegisterOperand, RegisterType.GeneralPurpose32Bit),
                new OperandDescriptor(OperandType.RegisterOrMemoryOperand, RegisterType.GeneralPurpose32Bit)),
            // CMP reg64, reg/mem64
            new X86OpcodeVariant(
                new byte[] { 0x3B },
                new OperandDescriptor(OperandType.RegisterOperand, RegisterType.GeneralPurpose64Bit),
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
