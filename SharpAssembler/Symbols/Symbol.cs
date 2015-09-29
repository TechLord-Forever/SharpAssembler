#region Copyright and License
/*
 * SharpAssembler
 * Library for .NET that assembles a predetermined list of
 * instructions into machine code.
 *
 * Copyright (C) 2011-2012 Daniël Pelsmaeker
 *
 * This file is part of SharpAssembler.
 *
 * SharpAssembler is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * SharpAssembler is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with SharpAssembler.  If not, see <http://www.gnu.org/licenses/>.
 */
#endregion
using System;
using System.Collections;
using System.Globalization;

namespace SharpAssembler.Symbols
{
    /// <summary>
    /// A symbol reference or definition.
    /// </summary>
    [Serializable]
    public sealed class Symbol : IAnnotatable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Symbol"/> class.
        /// </summary>
        /// <param name="symbolType">The type of symbol.</param>
        public Symbol(SymbolType symbolType)
            : this(symbolType, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Symbol"/> class.
        /// </summary>
        /// <param name="symbolType">The type of symbol.</param>
        /// <param name="identifier">The identifier of the symbol; or <see langword="null"/>.</param>
        public Symbol(SymbolType symbolType, string identifier)
        {
            SymbolType = symbolType;
            Identifier = identifier;
        }

        #region Properties
        private string identifier;
        /// <summary>
        /// Gets or sets the identifier of the symbol.
        /// </summary>
        /// <value>The identifier of the symbol; or <see langword="null"/> when no identifier was specified.</value>
        /// <remarks>
        /// When <see cref="Association"/> is set to an object implementing the <see cref="IIdentifiable"/>
        /// interface, then this property returns the identifier of that associated <see cref="IIdentifiable"/> object
        /// when it is set to <see langword="null"/>.
        /// </remarks>
        public string Identifier
        {
            get
            {
                var identifiable = Association as IIdentifiable;
                if (identifier == null && identifiable != null)
                    return identifiable.Identifier;
                else
                    return identifier;
            }
            set
            {
                identifier = value;
            }
        }

        /// <summary>
        /// Gets the label, block, section or file this <see cref="Symbol"/> is associated with.
        /// </summary>
        /// <value>The object that is associated with this symbol; or <see langword="null"/> when no object
        /// is associated with this symbol.</value>
        public IAssociatable Association { get; set; }

        /// <summary>
        /// Gets or sets the type of symbol.
        /// </summary>
        /// <value>A member of the <see cref="SymbolType"/> enumeration.</value>
        public SymbolType SymbolType { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="IFile"/> in which this symbol is defined.
        /// </summary>
        /// <value>The <see cref="IFile"/> in which this symbol is defined; or <see langword="null"/>.</value>
        /// <remarks>
        /// When <see cref="DefiningSection"/> is not <see langword="null"/>, this member should be the
        /// <see cref="ObjectFile"/> in which the section is defined.
        /// </remarks>
        public IFile DefiningFile { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="Section"/> in which this symbol is
        /// defined.
        /// </summary>
        /// <value>The <see cref="Section"/> in which this symbol is defined; or <see langword="null"/> when
        /// <see cref="IsAbsolute"/> is <see langword="true"/> or when the symbol is not defined in this object file
        /// (i.e. it is extern, <see cref="IsExtern"/> is <see langword="true"/>).</value>
        public Section DefiningSection { get; set; }

        /// <summary>
        /// Gets or sets whether the address of this symbol is an absolute value.
        /// </summary>
        /// <value><see langword="true"/> when the symbol is defined and its <see cref="Value"/> is an absolute
        /// address; otherwise, <see langword="false"/>.</value>
        public bool IsAbsolute { get; set; }

        /// <summary>
        /// Gets whether this symbol is defined in another file.
        /// </summary>
        /// <value><see langword="true"/> when the symbol is not defined in this file;
        /// otherwise, <see langword="false"/>.</value>
        public bool IsExtern
        {
            get { return DefiningSection == null && IsAbsolute == false; }
        }

        /// <summary>
        /// Gets or sets the value of the symbol.
        /// </summary>
        /// <value>The value of the symbol, which may be an absolute (when <see cref="IsAbsolute"/> is
        /// <see langword="true"/>) or relative address, or any other value.
        /// Otherwise, 0 when <see cref="IsExtern"/> is <see langword="true"/>.</value>
        public Int128 Value { get; set; }

        /// <summary>
        /// Gets or sets the size of the symbol's associated object.
        /// </summary>
        /// <value>The size of the symbol's associated objct; or 0 to specify no particular size.
        /// The default is 0.</value>
        public long Size { get; set; } = 0L;

        /// <inheritdoc />
        public IDictionary Annotations { get; set; } = new Hashtable();
        #endregion

        #region Methods
        /// <summary>
        /// Defines the symbol by setting its value and adding it to the symbol table.
        /// </summary>
        /// <param name="context">The current <see cref="Context"/>.</param>
        /// <param name="value">The value of the symbol.</param>
        public void Define(Context context, Int128 value)
        {
            Value = value;
            DefiningSection = context.Section;
            DefiningFile = context.Section.Parent;
            context.SymbolTable.Add(this);
        }

        /// <summary>
        /// Defines the symbol by setting its value and adding it to the symbol table.
        /// </summary>
        /// <param name="context">The current <see cref="Context"/>.</param>
        /// <param name="value">The value of the symbol.</param>
        public void Define(Context context, UInt128 value)
        {
            Define(context, (Int128)value);
        }

        /// <summary>
        /// References the symbol by adding it to the symbol table.
        /// </summary>
        /// <param name="context">The current <see cref="Context"/>.</param>
        public void ReferenceExtern(Context context)
        {
            context.SymbolTable.Add(this);
        }

        /// <summary>
        /// Sets an association between an object and a symbol.
        /// </summary>
        /// <param name="associatable">The associated object; or <see langword="null"/>.</param>
        /// <param name="symbol">The associated symbol; or <see langword="null"/>.</param>
        public static void SetAssociation(IAssociatable associatable, Symbol symbol)
        {
            if (symbol != null && associatable != null)
            {
                // Remove the associations between the specified symbol and its associatable,
                // and the specified associatable and its symbol.
                SetAssociation(symbol.Association, null);
                SetAssociation(associatable, null);

                symbol.Association = associatable;
                associatable.SetAssociatedSymbol(symbol);
            }
            else if (symbol != null)
            {
                if (symbol.Association != null)
                    symbol.Association.SetAssociatedSymbol(null);
                symbol.Association = null;
            }
            else if (associatable != null)
            {
                if (associatable.AssociatedSymbol != null)
                    associatable.AssociatedSymbol.Association = null;
                associatable.SetAssociatedSymbol(null);
            }
            // Else: both are null. We don't have to associate a null object with a null Symbol.
        }

        /// <summary>
        /// Sets an association between an object and a symbol.
        /// </summary>
        /// <param name="symbol">The associated symbol; or <see langword="null"/>.</param>
        /// <param name="associatable">The associated object; or <see langword="null"/>.</param>
        public static void SetAssociation(Symbol symbol, IAssociatable associatable)
        {
            SetAssociation(associatable, symbol);
        }

        /// <summary>
        /// Returns a <see cref="string"/> that represents the current <see cref="Object"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="string"/> that represents the current <see cref="Object"/>.
        /// </returns>
        public override string ToString()
        {
            if (identifier != null)
                return string.Format(CultureInfo.InvariantCulture, "<Symbol id=\"{0}\">", identifier);
            else
                return string.Format(CultureInfo.InvariantCulture, "<Symbol>");
        }
        #endregion
    }
}
