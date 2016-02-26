namespace SharpAssembler.Architectures.X86.Operands
{
    /// <summary>
    /// An operand for an instruction.
    /// </summary>
    public abstract class Operand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Operand"/> class.
        /// </summary>
        /// <param name="preferredSize">The preferred size of the operand.</param>
        protected Operand(DataSize preferredSize)
        {
            PreferredSize = preferredSize;
        }

        /// <inheritdoc />
        public virtual DataSize PreferredSize { get; set; }

        /// <inheritdoc />
        public virtual DataSize Size => PreferredSize;

        /// <summary>
        /// Constructs the operand's representation.
        /// </summary>
        /// <param name="context">The <see cref="Context"/> in which the operand is used.</param>
        /// <param name="instruction">The <see cref="EncodedInstruction"/> encoding the operand.</param>
        public abstract void Construct(Context context, EncodedInstruction instruction);

        /// <summary>
        /// Determines whether the specified <see cref="OperandDescriptor"/> matches this
        /// <see cref="Operand"/>.
        /// </summary>
        /// <param name="descriptor">The <see cref="OperandDescriptor"/> to match.</param>
        /// <returns><see langword="true"/> when the specified descriptor matches this operand;
        /// otherwise, <see langword="false"/>.</returns>
        public abstract bool IsMatch(OperandDescriptor descriptor);

        /// <summary>
        /// Adjusts this <see cref="Operand"/> based on the specified <see cref="OperandDescriptor"/>.
        /// </summary>
        /// <param name="descriptor">The <see cref="OperandDescriptor"/> used to adjust.</param>
        /// <remarks>
        /// Only <see cref="OperandDescriptor"/> instances for which <see cref="IsMatch"/> returns
        /// <see langword="true"/> may be used as a parameter to this method.
        /// </remarks>
        public virtual void Adjust(OperandDescriptor descriptor)
        {
        }
    }
}
