namespace SharpAssembler.Architectures.X86.Operands
{
    /// <summary>
    /// An operand for an instruction.
    /// </summary>
    public interface IConstructableOperand : IOperand
    {
        /// <summary>
        /// Constructs the operand's representation.
        /// </summary>
        /// <param name="context">The <see cref="Context"/> in which the operand is used.</param>
        /// <param name="instruction">The <see cref="EncodedInstruction"/> encoding the operand.</param>
        void Construct(Context context, EncodedInstruction instruction);

        /// <summary>
        /// Determines whether the specified <see cref="OperandDescriptor"/> matches this <see cref="Operand"/>.
        /// </summary>
        /// <param name="descriptor">The <see cref="OperandDescriptor"/> to match.</param>
        /// <returns><see langword="true"/> when the specified descriptor matches this operand;
        /// otherwise, <see langword="false"/>.</returns>
        bool IsMatch(OperandDescriptor descriptor);

        /// <summary>
        /// Adjusts this <see cref="Operand"/> based on the specified <see cref="OperandDescriptor"/>.
        /// </summary>
        /// <param name="descriptor">The <see cref="OperandDescriptor"/> used to adjust.</param>
        /// <remarks>
        /// Only <see cref="OperandDescriptor"/> instances for which <see cref="IsMatch"/> returns
        /// <see langword="true"/> may be used as a parameter to this method.
        /// </remarks>
        void Adjust(OperandDescriptor descriptor);
    }
}
