
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using SharpAssembler;
using SharpAssembler.Architectures.X86.Operands;

namespace SharpAssembler.Architectures.X86.Instructions
{
    /// <summary>
    /// The IN (Input from Port) instruction.
    /// </summary>
    public class In : X86Instruction
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="In"/> class.
        /// </summary>
        /// <param name="port">The port.</param>
        public In(RegisterOperand port)
            : this((Operand)port)
        {
            #region Contract
            Contract.Requires<ArgumentNullException>(port != null);
            #endregion
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="In"/> class.
        /// </summary>
        /// <param name="port">The port.</param>
        public In(Immediate port)
            : this((Operand)port)
        {
            #region Contract
            Contract.Requires<ArgumentNullException>(port != null);
            #endregion
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="In"/> class.
        /// </summary>
        /// <param name="port">The port.</param>
        private In(Operand port)
        {
            #region Contract
            Contract.Requires<ArgumentNullException>(port != null);
            Contract.Requires<InvalidCastException>(
                    port is EffectiveAddress ||
                    port is RegisterOperand);
            #endregion

            this.port = port;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the mnemonic of the instruction.
        /// </summary>
        /// <value>The mnemonic of the instruction.</value>
        public override string Mnemonic
        {
            get { return "in"; }
        }

        private Operand port;
        /// <summary>
        /// Gets the port.
        /// </summary>
        /// <value>An <see cref="Immediate"/> or <see cref="RegisterOperand"/>.</value>
        public Operand Port
        {
            get
            {
                #region Contract
                Contract.Ensures(Contract.Result<Operand>() != null);
                Contract.Ensures(
                    Contract.Result<Operand>() is Immediate ||
                    Contract.Result<Operand>() is RegisterOperand);
                #endregion
                return port;
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
                port = value;
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
            yield return this.port;
        }
        #endregion

        #region Instruction Variants
        /// <summary>
        /// An array of <see cref="X86OpcodeVariant"/> objects
        /// describing the possible variants of this instruction.
        /// </summary>
        private static X86OpcodeVariant[] variants = new[]{
            // IN AL, imm8
            new X86OpcodeVariant(
                new byte[] { 0xE4 },
                new OperandDescriptor(Register.AL),
                new OperandDescriptor(OperandType.Immediate, DataSize.Bit8)),
            // IN AX, imm8
            new X86OpcodeVariant(
                new byte[] { 0xE5 },
                new OperandDescriptor(Register.AX),
                new OperandDescriptor(OperandType.Immediate, DataSize.Bit8)),
            // IN EAX, imm8
            new X86OpcodeVariant(
                new byte[] { 0xE5 },
                new OperandDescriptor(Register.EAX),
                new OperandDescriptor(OperandType.Immediate, DataSize.Bit8)),

            // IN AL, DX
            new X86OpcodeVariant(
                new byte[] { 0xEC },
                new OperandDescriptor(Register.AL),
                new OperandDescriptor(Register.DX)),
            // IN AX, DX
            new X86OpcodeVariant(
                new byte[] { 0xED },
                new OperandDescriptor(Register.AX),
                new OperandDescriptor(Register.DX)),
            // IN EAX, DX
            new X86OpcodeVariant(
                new byte[] { 0xED },
                new OperandDescriptor(Register.EAX),
                new OperandDescriptor(Register.DX)),
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