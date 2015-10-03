namespace SharpAssembler.Symbols
{
    /// <summary>
    /// Specifies the type of a symbol.
    /// </summary>
    public enum SymbolType
    {
        /// <summary>
        /// The symbol's type is not specified.
        /// </summary>
        None = 0,
        /// <summary>
        /// The symbol is local to the object file.
        /// </summary>
        Private,
        /// <summary>
        /// The symbol is defined and available for other object files to refer to.
        /// </summary>
        Public,
        /// <summary>
        /// Similar to <see cref="Public"/> but with less precedence.
        /// </summary>
        Weak,
        /// <summary>
        /// The symbol is not defined but available in another object file.
        /// </summary>
        Extern,
    }
}
