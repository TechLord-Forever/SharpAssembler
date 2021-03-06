﻿using System;

namespace SharpAssembler.Symbols
{
    /// <summary>
    /// A reference to a symbol.
    /// </summary>
    public sealed class Reference
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Reference"/> class that references an associatable object.
        /// </summary>
        /// <param name="associatable">The <see cref="IAssociatable"/> being referenced.</param>
        /// <remarks>
        /// When the <see cref="IAssociatable"/> changes its <see cref="IAssociatable.AssociatedSymbol"/>,
        /// this reference is <em>not</em> updated or changed to reflect that, and will still be pointing at the
        /// previous, obsolete symbol.
        /// </remarks>
        public Reference(IAssociatable associatable)
        {
            Symbol = associatable.AssociatedSymbol;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Reference"/> class that references a symbol.
        /// </summary>
        /// <param name="symbol">The <see cref="Symbol"/> being referenced.</param>
        public Reference(Symbol symbol)
        {
            if (symbol == null)
                throw new ArgumentNullException(nameof(symbol));
            Symbol = symbol;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Reference"/> class that references a symbol by its identifier.
        /// </summary>
        /// <param name="symbolIdentifier">The identifier of the symbol being referenced.</param>
        public Reference(string symbolIdentifier)
        {
            this.symbolIdentifier = symbolIdentifier;
        }

        string symbolIdentifier;
        /// <summary>
        /// Gets the identifier of the referenced symbol.
        /// </summary>
        /// <value>An identifier.</value>
        public string SymbolIdentifier
        {
            get { return Symbol.Identifier; }
        }

        /// <summary>
        /// Gets the referenced <see cref="Symbol"/>.
        /// </summary>
        /// <value>The referenced <see cref="Symbol"/>; or <see langword="null"/> when the referenced symbol
        /// was not found.</value>
        public Symbol Symbol { get; }

        /// <summary>
        /// Gets whether this reference is resolved.
        /// </summary>
        /// <value><see langword="true"/> when the reference is resolved;
        /// otherwise, <see langword="false"/>.</value>
        public bool Resolved { get; } = false;

        /// <summary>
        /// Gets the object that is referenced.
        /// </summary>
        /// <value>The referenced object; or <see langword="null"/> when no specific object is referenced.</value>
        public IAssociatable Association => Symbol?.Association;

        /// <summary>
        /// Attempts to resolve the reference.
        /// </summary>
        /// <param name="context">The <see cref="Context"/> to use.</param>
        /// <returns><see langword="true"/> when the <see cref="Reference"/> now contains a resolved reference;
        /// otherwise, <see langword="false"/> when it contains an unresolved reference.</returns>
        public bool Resolve(Context context)
        {
            // Is the reference already resolved?
            if (Resolved)
                return true;

            return context.SymbolTable.Contains(Symbol);
        }

        /// <summary>
        /// Returns a <see cref="string"/> that represents the current <see cref="object"/>.
        /// </summary>
        /// <returns>A <see cref="string"/> that represents the current <see cref="object"/>.</returns>
        public override string ToString() => symbolIdentifier;

        /// <summary>
        /// Adds a constant offset to the value of the referenced symbol.
        /// </summary>
        /// <param name="left">The offset.</param>
        /// <param name="right">The referenced symbol.</param>
        /// <returns>The resulting <see cref="ReferenceOffset"/> value.</returns>
        public static ReferenceOffset operator +(long left, Reference right)
        {
            return new ReferenceOffset(right, left);
        }

        /// <summary>
        /// Adds a constant offset to the value of the referenced symbol.
        /// </summary>
        /// <param name="left">The referenced symbol.</param>
        /// <param name="right">The offset.</param>
        /// <returns>The resulting <see cref="ReferenceOffset"/> value.</returns>
        public static ReferenceOffset operator +(Reference left, long right)
        {
            return new ReferenceOffset(left, right);
        }
    }
}
