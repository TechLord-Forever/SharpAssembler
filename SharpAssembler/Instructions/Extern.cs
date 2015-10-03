using SharpAssembler.Symbols;
using System.Collections.Generic;

namespace SharpAssembler.Instructions
{
    /// <summary>
    /// Declares that a symbol is defined elsewhere.
    /// </summary>
    public class Extern : Constructable, IAssociatable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Extern"/> class.
        /// </summary>
        /// <param name="identifier">The identifier of the external symbol.</param>
        public Extern(string identifier)
            : this(identifier, 0)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Extern"/> class.
        /// </summary>
        /// <param name="identifier">The identifier of the external symbol.</param>
        /// <param name="length">The length of the symbol.</param>
        public Extern(string identifier, long length)
            : this(new Symbol(SymbolType.Extern, identifier){Size = length})
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Extern"/> class.
        /// </summary>
        /// <param name="symbol">The external symbol.</param>
        /// <remarks>
        /// Ensure that the symbol has a valid identifiers. Most object file formats do not support anonymous
        /// external symbols.
        /// </remarks>
        public Extern(Symbol symbol)
        {
            ExternSymbol = symbol;
        }

        private Symbol externSymbol;
        /// <summary>
        /// Gets or sets the symbol that is referenced by this instruction.
        /// </summary>
        /// <value>The <see cref="Symbol"/> that is referenced by this instruction;
        /// or <see langword="null"/>.</value>
        /// <remarks>
        /// When <see cref="ExternSymbol"/> is <see langword="null"/>, the instruction
        /// does not have any effect on the generated assembly.
        /// </remarks>
        public Symbol ExternSymbol
        {
            get { return externSymbol; }
            // NOTE: This property's field is set by the SetAssociatedSymbol() method.
            set { Symbol.SetAssociation(this, value); }
        }

        /// <inheritdoc />
        Symbol IAssociatable.AssociatedSymbol
        {
            get { return ExternSymbol; }
        }

        /// <inheritdoc />
        public override IEnumerable<IEmittable> Construct(Context context)
        {
            if (externSymbol != null)
                externSymbol.ReferenceExtern(context);

            yield break;
        }

        /// <inheritdoc />
        void IAssociatable.SetAssociatedSymbol(Symbol symbol)
        {
            externSymbol = symbol;
        }
    }
}
