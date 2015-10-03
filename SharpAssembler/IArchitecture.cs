namespace SharpAssembler
{
    /// <summary>
    /// A general interface for classes which describe a processor architecture.
    /// </summary>
    /// <remarks>
    /// Implementations of this interface may provide extra properties used to enable or disable certain features
    /// available in that processor's architecture.
    /// </remarks>
    public interface IArchitecture
    {
        /// <summary>
        /// Gets the name of the architecture.
        /// </summary>
        /// <value>The short, human readable name of the architecture.</value>
        string Name { get; }

        /// <summary>
        /// Gets the default address size used by this architecture.
        /// </summary>
        /// <value>A member of the <see cref="DataSize"/> enumeration.</value>
        /// <remarks>
        /// The address size may be overrided for individual instructions, depending on the architecture.
        /// </remarks>
        DataSize AddressSize { get; }

        /// <summary>
        /// Gets the default operand size used by this architecture.
        /// </summary>
        /// <value>A member of the <see cref="DataSize"/> enumeration.</value>
        /// <remarks>
        /// The operand size may be overrided for individual instructions, depending on the architecture.
        /// </remarks>
        DataSize OperandSize { get; }
    }
}
