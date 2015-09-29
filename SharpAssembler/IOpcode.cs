namespace SharpAssembler
{
    /// <summary>
    /// An opcode, which describes the semantics of an instruction.
    /// </summary>
    public interface IOpcode
    {
        /// <summary>
        /// Gets the mnemonic of the opcode.
        /// </summary>
        /// <value>The mnemonic of the opcode.</value>
        string Mnemonic    { get; }
    }
}
