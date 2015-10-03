using SharpAssembler.Symbols;
using System.Collections.Generic;

namespace SharpAssembler.Instructions
{
    /// <summary>
    /// A label, which defines a symbol.
    /// </summary>
    public class Label : Constructable, IAssociatable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Label"/> class
        /// that defines a private anonymous symbol.
        /// </summary>
        /// <remarks>
        /// The <see cref="DefinedSymbol"/> property holds the symbol that is defined.
        /// </remarks>
        public Label()
            : this(null, SymbolType.Private)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Label"/> class
        /// that defines a private symbol with the specified identifier.
        /// </summary>
        /// <param name="identifier">The identifier of the defined symbol; or <see langword="null"/>.</param>
        /// <remarks>
        /// The <see cref="DefinedSymbol"/> property holds the symbol that is defined.
        /// </remarks>
        public Label(string identifier)
            : this(identifier, SymbolType.Private)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Label"/> class
        /// that defines a symbol with the specified type and identifier.
        /// </summary>
        /// <param name="identifier">The identifier of the defined symbol.</param>
        /// <param name="symbolType">The type of symbol defined.</param>
        /// <remarks>
        /// The <see cref="DefinedSymbol"/> property holds the symbol that is defined.
        /// </remarks>
        public Label(string identifier, SymbolType symbolType)
            : this(new Symbol(symbolType, identifier))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Label"/> class
        /// that defines the specified symbol.
        /// </summary>
        /// <param name="symbol">The symbol that is defined.</param>
        /// <remarks>
        /// The <see cref="DefinedSymbol"/> property holds the symbol that is defined.
        /// </remarks>
        public Label(Symbol symbol)
        {
            DefinedSymbol = symbol;
        }

        private Symbol definedSymbol;
        /// <summary>
        /// Gets or sets the symbol that is defined by this instruction.
        /// </summary>
        /// <value>The <see cref="Symbol"/> that is defined by this instruction;
        /// or <see langword="null"/>.</value>
        /// <remarks>
        /// When <see cref="DefinedSymbol"/> is <see langword="null"/>, the instruction
        /// does not have any effect on the generated assembly.
        /// </remarks>
        public Symbol DefinedSymbol
        {
            get { return definedSymbol; }
            // NOTE: This property's field is set by the SetAssociatedSymbol() method.
            set { Symbol.SetAssociation(this, value); }
        }

        /// <inheritdoc />
        Symbol IAssociatable.AssociatedSymbol
        {
            get { return DefinedSymbol; }
        }

        /// <inheritdoc />
        public override IEnumerable<IEmittable> Construct(Context context)
        {
            if (definedSymbol != null)
                definedSymbol.Define(context, context.Address);

            yield break;
        }

        /// <inheritdoc />
        void IAssociatable.SetAssociatedSymbol(Symbol symbol)
        {
            definedSymbol = symbol;
        }
    }
}
