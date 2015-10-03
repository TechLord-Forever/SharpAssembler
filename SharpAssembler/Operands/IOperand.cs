namespace SharpAssembler.Architectures.X86.Operands
{
    /// <summary>
    /// An operand for an instruction.
    /// </summary>
    public interface IOperand
    {
        /// <summary>
        /// Gets or sets the preferred size of the operand.
        /// </summary>
        /// <value>A member of the <see cref="DataSize"/> enumeration;
        /// or <see cref="DataSize.None"/> to specify no size.</value>
        DataSize PreferredSize { get; set; }

        /// <summary>
        /// Gets the actual size of operand.
        /// </summary>
        /// <value>A member of the <see cref="DataSize"/> enumeration;
        /// or <see cref="DataSize.None"/>.</value>
        DataSize Size { get; }
    }
}
