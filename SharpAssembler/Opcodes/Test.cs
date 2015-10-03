
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using SharpAssembler;
using SharpAssembler.Architectures.X86.Operands;

namespace SharpAssembler.Architectures.X86.Instructions
{
    /// <summary>
    /// The TEST (Test Bits) instruction.
    /// </summary>
    public class Test : X86Instruction
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Test"/> class.
        /// </summary>
        /// <param name="destination">The destination register operand.</param>
        /// <param name="source">The source immediate value.</param>
        public Test(RegisterOperand destination, Immediate source)
            : this((Operand)destination, (Operand)source)
        {
            #region Contract
            Contract.Requires<ArgumentNullException>(destination != null);
            Contract.Requires<ArgumentNullException>(source != null);
            #endregion
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Test"/> class.
        /// </summary>
        /// <param name="destination">The destination memory operand.</param>
        /// <param name="source">The source immediate value.</param>
        public Test(EffectiveAddress destination, Immediate source)
            : this((Operand)destination, (Operand)source)
        {
            #region Contract
            Contract.Requires<ArgumentNullException>(destination != null);
            Contract.Requires<ArgumentNullException>(source != null);
            #endregion
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Test"/> class.
        /// </summary>
        /// <param name="destination">The destination register operand.</param>
        /// <param name="source">The source immediate register operand.</param>
        public Test(RegisterOperand destination, RegisterOperand source)
            : this((Operand)destination, (Operand)source)
        {
            #region Contract
            Contract.Requires<ArgumentNullException>(destination != null);
            Contract.Requires<ArgumentNullException>(source != null);
            #endregion
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Test"/> class.
        /// </summary>
        /// <param name="destination">The destination memory operand.</param>
        /// <param name="source">The source immediate register operand.</param>
        public Test(EffectiveAddress destination, RegisterOperand source)
            : this((Operand)destination, (Operand)source)
        {
            #region Contract
            Contract.Requires<ArgumentNullException>(destination != null);
            Contract.Requires<ArgumentNullException>(source != null);
            #endregion
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Test"/> class.
        /// </summary>
        /// <param name="destination">The destination operand.</param>
        /// <param name="source">The source operand.</param>
        private Test(Operand destination, Operand source)
        {
            #region Contract
            Contract.Requires<ArgumentNullException>(destination != null);
            Contract.Requires<InvalidCastException>(
                    destination is EffectiveAddress ||
                    destination is RegisterOperand);
            Contract.Requires<ArgumentNullException>(source != null);
            Contract.Requires<InvalidCastException>(
                    source is Immediate ||
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
            get { return "test"; }
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
                    Contract.Result<Operand>() is Immediate ||
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
                    value is Immediate ||
                    value is RegisterOperand);
                #endregion
                source = value;
            }
#endif
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
            // TEST AL, imm8
            new X86OpcodeVariant(
                new byte[] { 0xA8 },
                new OperandDescriptor(Register.AL),
                new OperandDescriptor(OperandType.Immediate, DataSize.Bit8)),
            // TEST AX, imm16
            new X86OpcodeVariant(
                new byte[] { 0xA9 },
                new OperandDescriptor(Register.AX),
                new OperandDescriptor(OperandType.Immediate, DataSize.Bit16)),
            // TEST EAX, imm32
            new X86OpcodeVariant(
                new byte[] { 0xA9 },
                new OperandDescriptor(Register.EAX),
                new OperandDescriptor(OperandType.Immediate, DataSize.Bit32)),
            // TEST RAX, imm32
            new X86OpcodeVariant(
                new byte[] { 0xA9 },
                new OperandDescriptor(Register.RAX),
                new OperandDescriptor(OperandType.Immediate, DataSize.Bit32)),

            // TEST reg/mem8, imm8
            new X86OpcodeVariant(
                new byte[] { 0xF6 }, 0,
                new OperandDescriptor(OperandType.RegisterOrMemoryOperand, RegisterType.GeneralPurpose8Bit),
                new OperandDescriptor(OperandType.Immediate, DataSize.Bit8)),
            // TEST reg/mem16, imm16
            new X86OpcodeVariant(
                new byte[] { 0xF7 }, 0,
                new OperandDescriptor(OperandType.RegisterOrMemoryOperand, RegisterType.GeneralPurpose16Bit),
                new OperandDescriptor(OperandType.Immediate, DataSize.Bit16)),
            // TEST reg/mem32, imm32
            new X86OpcodeVariant(
                new byte[] { 0xF7 }, 0,
                new OperandDescriptor(OperandType.RegisterOrMemoryOperand, RegisterType.GeneralPurpose32Bit),
                new OperandDescriptor(OperandType.Immediate, DataSize.Bit32)),
            // TEST reg/mem64, imm32
            new X86OpcodeVariant(
                new byte[] { 0xF7 }, 0,
                new OperandDescriptor(OperandType.RegisterOrMemoryOperand, RegisterType.GeneralPurpose64Bit),
                new OperandDescriptor(OperandType.Immediate, DataSize.Bit32)),

            
            // TEST reg/mem8, reg8
            new X86OpcodeVariant(
                new byte[] { 0x84 },
                new OperandDescriptor(OperandType.RegisterOrMemoryOperand, RegisterType.GeneralPurpose8Bit),
                new OperandDescriptor(OperandType.RegisterOperand, RegisterType.GeneralPurpose8Bit)),
            // TEST reg/mem16, reg16
            new X86OpcodeVariant(
                new byte[] { 0x85 },
                new OperandDescriptor(OperandType.RegisterOrMemoryOperand, RegisterType.GeneralPurpose16Bit),
                new OperandDescriptor(OperandType.RegisterOperand, RegisterType.GeneralPurpose16Bit)),
            // TEST reg/mem32, reg32
            new X86OpcodeVariant(
                new byte[] { 0x85 },
                new OperandDescriptor(OperandType.RegisterOrMemoryOperand, RegisterType.GeneralPurpose32Bit),
                new OperandDescriptor(OperandType.RegisterOperand, RegisterType.GeneralPurpose32Bit)),
            // TEST reg/mem64, reg64
            new X86OpcodeVariant(
                new byte[] { 0x85 },
                new OperandDescriptor(OperandType.RegisterOrMemoryOperand, RegisterType.GeneralPurpose64Bit),
                new OperandDescriptor(OperandType.RegisterOperand, RegisterType.GeneralPurpose64Bit)),
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
