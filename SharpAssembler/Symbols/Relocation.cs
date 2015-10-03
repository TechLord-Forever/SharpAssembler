using System.Collections;
using System.Globalization;

namespace SharpAssembler.Symbols
{
    /// <summary>
    /// Specifies a single relocation.
    /// </summary>
    public sealed class Relocation
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Relocation"/> class.
        /// </summary>
        /// <param name="symbol">The target symbol.</param>
        /// <param name="offset">The offset relative to the start of at which the storage
        /// unit to be relocated resides.</param>
        /// <param name="addend">The constant used to compute the value of the relocatable field.</param>
        /// <param name="type">The type of relocation compution to perform.</param>
        public Relocation(Symbol symbol, long offset, long addend, RelocationType type)
        {
            TargetSymbol = symbol;
            Offset = offset;
            Addend = addend;
            Type = type;
        }

        /// <summary>
        /// Gets or sets the target symbol of this relocation.
        /// </summary>
        /// <value>A <see cref="Symbol"/>.</value>
        /// <remarks>
        /// When the target symbol is a defined symbol (as opposed to a symbol which is not defined in this object
        /// file), the resulting relocation (as encoded by the file format) may have a value which equals the offset
        /// of the symbol relative to it's section, and a symbol specifier which targets that section.
        /// </remarks>
        public Symbol TargetSymbol { get; set; }

        /// <summary>
        /// Gets or sets the offset of the storage unit to be relocated, relative to the start of the section
        /// containing the storage unit.
        /// </summary>
        public long Offset { get; set; }

        /// <summary>
        ///
        /// </summary>
        public int Length { get; set; }

        /// <summary>
        /// Gets or sets a constant addend used to compute the value to be stored into the relocatable field.
        /// </summary>
        /// <value>The addend.</value>
        public long Addend { get; set; }

        /// <summary>
        /// Gets or sets the relocation type.
        /// </summary>
        /// <value>A member of the <see cref="RelocationType"/> enumeration.</value>
        public RelocationType Type { get; set; }

        /// <summary>
        /// Gets a dictionary which may be used to store data specific to this object.
        /// </summary>
        /// <value>An implementation of the <see cref="IDictionary"/> interface.</value>
        /// <remarks>
        /// This property is not serialized or deserialized.
        /// </remarks>
        public IDictionary Annotations { get; private set; } = new Hashtable();

        /// <summary>
        /// Returns a <see cref="string"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="string"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return $"<Relocation [0x{Offset:X} -> {TargetSymbol.Identifier}>";
        }
    }
}
