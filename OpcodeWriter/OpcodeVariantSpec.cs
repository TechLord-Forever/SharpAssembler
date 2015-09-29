using System.Collections.ObjectModel;

namespace OpcodeWriter
{
    /// <summary>
    /// A specification for a single variant of an opcode.
    /// </summary>
    public class OpcodeVariantSpec
    {
        /// <summary>
        /// Gets or sets whether this opcode variant has no associated mnemonic.
        /// </summary>
        /// <value><see langword="true"/> when the opcode variant has no associated mnemonic;
        /// otherwise, <see langword="false"/>. The default is <see langword="false"/>.</value>
        public bool HasNoMnemonic { get; set; } = false;

        /// <summary>
        /// Gets or sets the bytes that are used for the encoded opcode part of the instruction.
        /// </summary>
        /// <value>An array of bytes; or <see langword="null"/> to specify no opcode.
        /// The default is <see langword="null"/>.</value>
        public byte[] OpcodeBytes { get; set; } = null;

        /// <summary>
        /// Gets a collection of operands.
        /// </summary>
        /// <value>A collection of operands.</value>
        public Collection<OperandSpec> Operands { get; private set; } = new Collection<OperandSpec>();
    }
}
