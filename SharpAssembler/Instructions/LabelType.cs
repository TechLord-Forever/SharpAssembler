namespace SharpAssembler.Instructions
{
    /// <summary>
    /// Specifies the type of symbol a label creates.
    /// </summary>
    public enum LabelType
    {
        /// <summary>
        /// A private symbol. This is the default.
        /// </summary>
        Private = 0,
        /// <summary>
        /// A weak public symbol.
        /// </summary>
        Weak,
        /// <summary>
        /// A strong public symbol.
        /// </summary>
        Public,
    }
}
