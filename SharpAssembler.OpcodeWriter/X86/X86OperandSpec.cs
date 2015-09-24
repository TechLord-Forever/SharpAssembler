namespace SharpAssembler.OpcodeWriter.X86
{
    /// <summary>
    /// Describes an x86-64 opcode variant.
    /// </summary>
    public class X86OperandSpec : OperandSpec
	{
        /// <summary>
        /// Gets or sets the size of the operand.
        /// </summary>
        /// <value>A member of the <see cref="DataSize"/> enumeration.
        /// The default is <see cref="SharpAssembler.OpcodeWriter.DataSize.None"/>.</value>
        public DataSize Size { get; set; } = DataSize.None;

        /// <summary>
        /// Gets or sets the fixed register that is used for this operand.
        /// </summary>
        /// <value>A member of the <see cref="Register"/> enumeration;
        /// or <see cref="SharpAssembler.OpcodeWriter.X86.Register.None"/> to specify none.
        /// The default is <see cref="SharpAssembler.OpcodeWriter.X86.Register.None"/>.</value>
        public Register FixedRegister { get; set; } = Register.None;

        /// <summary>
        /// Gets or sets the type of operand.
        /// </summary>
        /// <value>A member of the <see cref="X86OperandType"/> enumeration;
        /// or <see cref="SharpAssembler.OpcodeWriter.X86.X86OperandType.None"/> to specify none.
        /// The default is <see cref="SharpAssembler.OpcodeWriter.X86.X86OperandType.None"/>.</value>
        public X86OperandType Type { get; set; } = X86OperandType.None;

        /// <summary>
        /// Gets or sets the encoding of the operand.
        /// </summary>
        /// <value>A member of the operand encoding enumeration;
        /// or <see cref="SharpAssembler.OpcodeWriter.X86.X86OperandEncoding.Default"/> to specify the default.
        /// The default is <see cref="SharpAssembler.OpcodeWriter.X86.X86OperandEncoding.Default"/>.</value>
        public X86OperandEncoding Encoding { get; set; } = X86OperandEncoding.Default;
    }
}
