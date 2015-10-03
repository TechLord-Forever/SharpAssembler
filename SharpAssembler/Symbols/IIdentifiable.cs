namespace SharpAssembler.Symbols
{
    /// <summary>
    /// An interface for classes which provide an identifier.
    /// </summary>
    public interface IIdentifiable : IAssociatable
    {
        /// <summary>
        /// Gets the identifier of this <see cref="IIdentifiable"/>.
        /// </summary>
        /// <value>An identifier, never <see langword="null"/>.</value>
        string Identifier { get; }
    }
}
