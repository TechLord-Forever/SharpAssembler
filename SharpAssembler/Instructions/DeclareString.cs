﻿#region Copyright and License
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
using System.Text;
using System.Collections.Generic;

namespace SharpAssembler.Instructions
{
    /// <summary>
    /// Declares a string.
    /// </summary>
    public class DeclareString : Constructable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeclareString"/> class declaring the specified string
        /// with UTF-8 encoding.
        /// </summary>
        /// <param name="data">The data.</param>
        public DeclareString(string data)
            : this(data, Encoding.UTF8)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DeclareString"/> class declaring the specified string
        /// with the specified encoding.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="encoding">The encoding of the string.</param>
        public DeclareString(string data, Encoding encoding)
        {
            Data = data;
            Encoding = encoding;
        }

        /// <summary>
        /// Gets or sets the string that will be declared.
        /// </summary>
        /// <value>A string.</value>
        public string Data { get; private set; }

        /// <summary>
        /// Gets or sets the encoding of the declared string.
        /// </summary>
        /// <value>An <see cref="Encoding"/>.</value>
        public Encoding Encoding { get; private set; }

        /// <inheritdoc />
        public override IEnumerable<IEmittable> Construct(Context context)
        {
            yield return new RawEmittable(Encoding.GetBytes(Data));
        }

        /// <inheritdoc />
        public override void Accept(IObjectFileVisitor visitor)
        {
            visitor.VisitDeclareString(this);
        }
    }
}
