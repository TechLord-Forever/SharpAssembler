using System.Collections.Generic;
using System.Diagnostics.Contracts;
using SharpAssembler;
using SharpAssembler.Architectures.X86.Operands;

namespace SharpAssembler.Architectures.X86.Instructions
{
    /// <summary>
    /// The AAM (ASCII Adjust After Multiply) instruction.
    /// </summary>
    public class Aam : X86Instruction
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Aam"/> class.
        /// </summary>
        public Aam()
            : this(null)
        { /* Nothing to do. */ }

        /// <summary>
        /// Initializes a new instance of the <see cref="Aam"/> class.
        /// </summary>
        /// <param name="base">The base of the operation.</param>
        public Aam(Immediate @base)
        {
            this.@base = @base;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the mnemonic of the instruction.
        /// </summary>
        /// <value>The mnemonic of the instruction.</value>
        public override string Mnemonic
        {
            get { return "aam"; }
        }

        /// <summary>
        /// Gets whether this instruction is valid in 64-bit mode.
        /// </summary>
        /// <value><see langword="true"/> when the instruction is valid in 64-bit mode;
        /// otherwise, <see langword="false"/>.</value>
        public override bool ValidIn64BitMode
        {
            get { return false; }
        }

        private Immediate @base;
        /// <summary>
        /// Gets the base of the operation.
        /// </summary>
        /// <value>An <see cref="Immediate"/> operand;
        /// or <see langword="null"/> to use base 10.</value>
        public Immediate Base
        {
            get { return @base; }
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
            yield return this.@base;
        }
        #endregion

        #region Instruction Variants
        /// <summary>
        /// An array of <see cref="X86Instruction.X86OpcodeVariant"/> objects
        /// describing the possible variants of this instruction.
        /// </summary>
        private static X86OpcodeVariant[] variants = new[]{
            // AAD
            new X86OpcodeVariant(
                new byte[] { 0xD4, 0x0A }),
            // (None) imm8
            new X86OpcodeVariant(
                new byte[] { 0xD4 },
                new OperandDescriptor(OperandType.Immediate, DataSize.Bit8)),
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
