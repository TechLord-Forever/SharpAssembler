using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using SharpAssembler;
using SharpAssembler.Architectures.X86.Operands;

namespace SharpAssembler.Architectures.X86.Instructions.x87
{
    /// <summary>
    /// The FADD (Floating-Point Add) instruction.
    /// </summary>
    public class FAdd : X86Instruction
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="FAdd"/> class.
        /// </summary>
        /// <param name="destination">The x87 register being modified.</param>
        /// <param name="source">The x87 register with the value to add.</param>
        public FAdd(RegisterOperand destination, RegisterOperand source)
            : this(destination, (Operand)source)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FAdd"/> class.
        /// </summary>
        /// <param name="destination">The x87 register being modified.</param>
        /// <param name="source">The memory location of the value to add.</param>
        public FAdd(RegisterOperand destination, EffectiveAddress source)
            : this(destination, (Operand)source)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FAdd"/> class.
        /// </summary>
        /// <param name="destination">The x87 register being modified.</param>
        /// <param name="source">The value to add.</param>
        private FAdd(RegisterOperand destination, Operand source)
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
            get { return "fadd"; }
        }

        private RegisterOperand destination;
        /// <summary>
        /// Gets the destination of the result of the instruction.
        /// </summary>
        /// <value>A <see cref="RegisterOperand"/>.</value>
        public RegisterOperand Destination
        {
            get
            {
                return destination;
            }
        }

        private Operand source;
        /// <summary>
        /// Gets the source of the value used by the instruction.
        /// </summary>
        /// <value>A <see cref="Operand"/>.</value>
        public Operand Source
        {
            get
            {
                return source;
            }
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
            yield return destination;
            yield return source;
        }
        #endregion

        #region Instruction Variants
        /// <summary>
        /// An array of <see cref="X86OpcodeVariant"/> objects
        /// describing the possible variants of this instruction.
        /// </summary>
        private static X86OpcodeVariant[] variants = new[]{
            // FADD ST(0), ST(i)
            new X86OpcodeVariant(
                new byte[] { 0xD8, 0xC0 },
                new OperandDescriptor(Register.ST0),
                new OperandDescriptor(OperandType.RegisterOperand, RegisterType.FloatingPoint,
                    OperandEncoding.OpcodeAdd)),
            // FADD ST(i), ST(0)
            new X86OpcodeVariant(
                new byte[] { 0xDC, 0xC0 },
                new OperandDescriptor(OperandType.RegisterOperand, RegisterType.FloatingPoint,
                    OperandEncoding.OpcodeAdd),
                new OperandDescriptor(Register.ST0)),

            // FADD mem32real
            new X86OpcodeVariant(
                new byte[] { 0xD8 }, 0,
                new OperandDescriptor(Register.ST0),
                new OperandDescriptor(OperandType.MemoryOperand, DataSize.Bit32)),
            // FADD mem64real
            new X86OpcodeVariant(
                new byte[] { 0xDC }, 0,
                new OperandDescriptor(Register.ST0),
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
