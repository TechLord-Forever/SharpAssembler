namespace OpcodeWriter
{
    /// <summary>
    /// Specifies the state of the <see cref="ScriptInterpreter"/>.
    /// </summary>
    internal enum ReaderState
    {
        /// <summary>
        /// No more states.
        /// </summary>
        Finished = -1,

        /// <summary>
        /// The initial state.
        /// </summary>
        Initial = 0,

        /// <summary>
        /// Expects an object definition.
        /// </summary>
        ObjectDefinition,
    }
}
