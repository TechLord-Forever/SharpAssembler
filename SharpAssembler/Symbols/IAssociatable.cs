namespace SharpAssembler.Symbols
{
    /// <summary>
    /// Represents a structure that has an associated symbol.
    /// </summary>
    public interface IAssociatable
    {
        /// <summary>
        /// Gets the <see cref="Symbol"/> associated with this <see cref="IAssociatable"/>.
        /// </summary>
        /// <value>A <see cref="Symbol"/>.</value>
        /// <remarks>
        /// If an implementation provides a setter for this property,
        /// call the static <see cref="Symbol.SetAssociation(IAssociatable, Symbol)"/> method.
        /// </remarks>
        Symbol AssociatedSymbol { get; }

        /// <summary>
        /// Sets the symbol that is associated with this object.
        /// </summary>
        /// <param name="symbol">The associated symbol; or <see langword="null"/> when no symbol is associated
        /// with this object.</param>
        /// <remarks>
        /// Implementations should only set the associated symbol in this class.
        /// This method should be implemented explicitly.
        /// </remarks>
        /// <exception cref="System.NotSupportedException">
        /// The associated symbol cannot be set.
        /// </exception>
        void SetAssociatedSymbol(Symbol symbol);
    }
}
