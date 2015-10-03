using System;

namespace SharpAssembler.Architectures.X86.Operands
{
    /// <summary>
    /// A register operand.
    /// </summary>
    /// <remarks>
    /// In the Intel manuals, a register operand is denoted as <c>r8</c>, <c>r16</c>, <c>r32</c> or <c>r64</c>.
    /// </remarks>
    public partial class RegisterOperand : Operand, IOperand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RegisterOperand"/> class.
        /// </summary>
        /// <param name="register">The <see cref="Register"/>; or <see langword="Register.None"/>.</param>
        public RegisterOperand(Register register)
            : base(DataSize.None)
        {
            Register = register;
        }

        /// <summary>
        /// Gets the register.
        /// </summary>
        /// <value>A <see cref="Register"/>.</value>
        public Register Register { get; set; }

        /// <summary>
        /// Gets the requested size of the operand.
        /// </summary>
        /// <value>A member of the <see cref="DataSize"/> enumeration; or <see cref="DataSize.None"/> to specify no
        /// size.</value>
        /// <exception cref="NotSupportedException">
        /// The requested size cannot be set.
        /// </exception>
        public override DataSize PreferredSize
        {
            get { return DataSize.None; }
            set { throw new NotSupportedException(); }
        }

        /// <summary>
        /// Gets the actual size of the relative offset value.
        /// </summary>
        /// <value>A member of the <see cref="DataSize"/> enumeration; or <see cref="DataSize.None"/>.</value>
        public override DataSize Size
        {
            get { return Register.Size; }
        }

        /// <summary>
        /// Gets or sets how the operand is to be encoded.
        /// </summary>
        /// <value>A member of the <see cref="OperandEncoding"/> enumeration.
        /// The default is <see cref="RegisterOperand.OperandEncoding.Default"/>.</value>
        internal OperandEncoding Encoding { get; set; } = OperandEncoding.Default;

        /// <summary>
        /// Constructs the operand's representation.
        /// </summary>
        /// <param name="context">The <see cref="Context"/> in which the operand is used.</param>
        /// <param name="instruction">The <see cref="EncodedInstruction"/> encoding the operand.</param>
        internal override void Construct(Context context, EncodedInstruction instruction)
        {
            if (context.AddressingMode != DataSize.Bit64 && Register.Size == DataSize.Bit64)
                throw new AssemblerException($"The 64-bit register {Register} cannot be used with non-64-bit operand sizes.");

            // Encode the register as part of the opcode or ModRM byte.
            switch (Encoding)
            {
                case OperandEncoding.Default:
                    instruction.SetModRMByte();
                    instruction.ModRM.Reg = Register.Value;
                    break;
                case OperandEncoding.AddToOpcode:
                    instruction.OpcodeReg = Register.Value;
                    break;
                case OperandEncoding.ModRm:
                    instruction.SetModRMByte();
                    instruction.ModRM.Mod = 0x03;
                    instruction.ModRM.RM = Register.Value;
                    break;
                case OperandEncoding.Ignore:
                    // The operand is ignored.
                    break;
            }

            // Set the operand size to the size of the register.
            instruction.SetOperandSize(context.AddressingMode, Register.Size);
        }

        /// <summary>
        /// Determines whether the specified <see cref="OperandDescriptor"/> matches this
        /// <see cref="Operand"/>.
        /// </summary>
        /// <param name="descriptor">The <see cref="OperandDescriptor"/> to match.</param>
        /// <returns><see langword="true"/> when the specified descriptor matches this operand;
        /// otherwise, <see langword="false"/>.</returns>
        internal override bool IsMatch(OperandDescriptor descriptor)
        {
            switch (descriptor.OperandType)
            {
                case OperandType.RegisterOrMemoryOperand:
                case OperandType.RegisterOperand:
                    return descriptor.RegisterType.HasFlag(Register.Type);
                case OperandType.FixedRegister:
                    return Register == descriptor.FixedRegister;
                default:
                    return false;
            }
        }

        /// <summary>
        /// Adjusts this <see cref="Operand"/> based on the specified
        /// <see cref="OperandDescriptor"/>.
        /// </summary>
        /// <param name="descriptor">The <see cref="OperandDescriptor"/> used to
        /// adjust.</param>
        /// <remarks>
        /// Only <see cref="OperandDescriptor"/> instances for which <see cref="IsMatch"/>
        /// returns <see langword="true"/> may be used as a parameter to this method.
        /// </remarks>
        internal override void Adjust(OperandDescriptor descriptor)
        {
            // When the operand needs to be added to the opcode, set it as such.
            if (descriptor.OperandEncoding == X86.OperandEncoding.OpcodeAdd)
                Encoding = OperandEncoding.AddToOpcode;
            else if (descriptor.OperandType == OperandType.RegisterOrMemoryOperand)
                Encoding = OperandEncoding.ModRm;
            else if (descriptor.OperandType == OperandType.FixedRegister)
                Encoding = OperandEncoding.Ignore;
            else
                Encoding = OperandEncoding.Default;
        }

        /// <summary>
        /// Returns a <see cref="string"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="string"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return Enum.GetName(typeof(Register), Register);
        }

        /// <summary>
        /// Converts a <see cref="Register"/> to a <see cref="RegisterOperand"/>.
        /// </summary>
        /// <param name="register">The <see cref="Register"/> to convert.</param>
        /// <returns>The resulting <see cref="RegisterOperand"/>.</returns>
        public static implicit operator RegisterOperand(Register register)
        {
            return new RegisterOperand(register);
        }
    }
}
