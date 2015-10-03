using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SharpAssembler.Symbols
{
    /// <summary>
    /// The symbol table. Symbols can be retrieved by name, and are enumerated by virtual address.
    /// </summary>
    /// <remarks>
    /// It is possible for a symbol to have no name, and for two symbols to have the same name.
    /// </remarks>
    public class SymbolTable : Collection<Symbol>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SymbolTable"/> class.
        /// </summary>
        public SymbolTable()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SymbolTable"/> class.
        /// </summary>
        /// <param name="symbols">The items to add to this collection.</param>
        public SymbolTable(IEnumerable<Symbol> symbols)
        {
            AddRange(symbols);
        }

        /// <summary>
        /// Adds a range of symbols to the symbol table.
        /// </summary>
        /// <param name="symbols">The symbols to add.</param>
        public void AddRange(IEnumerable<Symbol> symbols)
        {
            foreach (Symbol symbol in symbols)
            {
                Add(symbol);
            }
        }

        /// <inheritdoc />
        protected override void InsertItem(int index, Symbol item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            base.InsertItem(index, item);
        }

        /// <inheritdoc />
        protected override void SetItem(int index, Symbol item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            base.SetItem(index, item);
        }

        /// <summary>
        /// Checks whether the collection contains a <see cref="Symbol"/> with a particular identifier.
        /// </summary>
        /// <param name="identifier">The identifier to look for.</param>
        /// <returns><see langword="true"/> when the collection has a symbol with the specified identifier;
        /// otherwise, <see langword="false"/>.</returns>
        /// <remarks>
        /// Symbols that have no identifier (their <see cref="Symbol.Identifier"/> is <see langword="null"/>)
        /// cannot be located using this method.
        /// </remarks>
        public bool Contains(string identifier)
        {
            return IndexOf(identifier) != -1;
        }

        /// <summary>
        /// Returns the index of the first symbol with the specified identifier.
        /// </summary>
        /// <param name="identifier">The identifier to look for.</param>
        /// <returns>The zero-based index of the symbol in this table;
        /// or -1 when no symbol with the specified identifier was found.</returns>
        /// <remarks>
        /// Symbols that have no identifier (their <see cref="Symbol.Identifier"/> is <see langword="null"/>)
        /// cannot be located using this method.
        /// </remarks>
        public int IndexOf(string identifier)
        {
            for (int i = 0; i < Count; i++)
            {
                if (this[i].Identifier == identifier)
                    return i;
            }
            return -1;
        }


        /// <summary>
        /// Gets the first symbol with the specified identifier.
        /// </summary>
        /// <param name="identifier">The identifier to look for.</param>
        /// <returns>The <see cref="Symbol"/> with the specified identifier;
        /// or <see langword="null"/> when not found.</returns>
        /// <remarks>
        /// Symbols that have no identifier (their <see cref="Symbol.Identifier"/> is <see langword="null"/>)
        /// cannot be located using this method.
        /// </remarks>
        public Symbol this[string identifier]
        {
            get
            {
                int index = IndexOf(identifier);
                if (index < 0)
                    return null;
                return this[index];
            }
        }
    }
}
