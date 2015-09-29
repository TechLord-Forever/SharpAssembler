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
using System.Collections;
using System.Globalization;

namespace SharpAssembler.Symbols
{
    /// <summary>
    /// Specifies a single relocation.
    /// </summary>
    public sealed class Relocation// : IAnnotatable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Relocation"/> class.
        /// </summary>
        /// <param name="symbol">The target symbol.</param>
        /// <param name="offset">The offset relative to the start of at which the storage
        /// unit to be relocated resides.</param>
        /// <param name="addend">The constant used to compute the value of the relocatable field.</param>
        /// <param name="type">The type of relocation compution to perform.</param>
        public Relocation(Symbol symbol, Int128 offset, Int128 addend, RelocationType type)
        {
            TargetSymbol = symbol;
            Offset = offset;
            Addend = addend;
            Type = type;
        }

        #region Properties
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
        public Int128 Offset { get; set; }

        /// <summary>
        ///
        /// </summary>
        public int Length { get; set; }

        /// <summary>
        /// Gets or sets a constant addend used to compute the value to be stored into the relocatable field.
        /// </summary>
        /// <value>The addend.</value>
        public Int128 Addend { get; set; }

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

        #endregion

        #region Methods
        /// <summary>
        /// Returns a <see cref="string"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="string"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return string.Format(
                CultureInfo.InvariantCulture,
                "<Relocation [0x{1:X} -> {3}>",
                Offset,
                TargetSymbol.Identifier);
        }
        #endregion
    }
}
