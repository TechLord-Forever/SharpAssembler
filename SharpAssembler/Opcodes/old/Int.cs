
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using SharpAssembler;
using SharpAssembler.Architectures.X86.Operands;

namespace SharpAssembler.Architectures.X86.Instructions
{
    /// <summary>
    /// The INT (Interrupt to Vector) instruction.
    /// </summary>
    public class Int : X86Instruction
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Int"/> class.
        /// </summary>
        /// <param name="interruptVector">The interrupt vector.</param>
        public Int(Immediate interruptVector)
        {
            #region Contract
            Contract.Requires<ArgumentNullException>(interruptVector != null);
            #endregion
            this.interruptVector = interruptVector;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the mnemonic of the instruction.
        /// </summary>
        /// <value>The mnemonic of the instruction.</value>
        public override string Mnemonic
        {
            get { return "int"; }
        }

        private Immediate interruptVector;
        /// <summary>
        /// Gets the interrupt vector.
        /// </summary>
        /// <value>A <see cref="Immediate"/>.</value>
        public Immediate InterruptVector
        {
            get
            {
                #region Contract
                Contract.Ensures(Contract.Result<Immediate>() != null);
                #endregion
                return (Immediate)interruptVector;
            }
#if OPERAND_SET
            set
            {
                #region Contract
                Contract.Requires<ArgumentNullException>(value != null);
                #endregion
                interruptVector = value;
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
            yield return this.interruptVector;
        }
        #endregion

        #region Instruction Variants
        /// <summary>
        /// An array of <see cref="X86OpcodeVariant"/> objects
        /// describing the possible variants of this instruction.
        /// </summary>
        private static X86OpcodeVariant[] variants = new[]{
            // INT imm8
            new X86OpcodeVariant(
                new byte[] { 0xCD },
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



