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
using System.IO;

namespace SharpAssembler
{
    /// <summary>
    /// Interface for classes that can assemble an object file into its binary representation.
    /// </summary>
    public abstract class ObjectFileAssembler
    {
        /// <summary>
        /// Gets the <see cref="ObjectFile"/> that will be assembled.
        /// </summary>
        /// <value>An <see cref="ObjectFile"/>.</value>
        protected ObjectFile ObjectFile { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectFileAssembler"/> class.
        /// </summary>
        /// <param name="objectFile">The <see cref="ObjectFile"/> that will be assembled.</param>
        protected ObjectFileAssembler(ObjectFile objectFile)
        {
            ObjectFile = objectFile;
        }

        /// <summary>
        /// Assembles the object file and writes the resulting binary representation
        /// to the specified <see cref="BinaryWriter"/>.
        /// </summary>
        /// <param name="path">The path to a file to which the binary representation is written.</param>
        /// <remarks>
        /// When a file already exists at the specified path, it is overwritten.
        /// </remarks>
        public void Assemble(string path)
        {
            using (var stream = File.Create(path))
                Assemble(stream);
        }

        /// <summary>
        /// Assembles the object file and writes the resulting binary representation
        /// to the specified <see cref="Stream"/>.
        /// </summary>
        /// <param name="stream">The <see cref="Stream"/> to which the binary representation is written.</param>
        /// <remarks>
        /// The stream is not closed after this method completes.
        /// </remarks>
        /// <exception cref="ArgumentException">
        /// <paramref name="stream"/> is closed.
        /// </exception>
        public void Assemble(Stream stream)
        {
            using (var writer = new BinaryWriter(new NonClosingStreamWrapper(stream)))
                Assemble(writer);
        }

        /// <summary>
        /// Assembles the object file and writes the resulting binary representation
        /// to the specified <see cref="BinaryWriter"/>.
        /// </summary>
        /// <param name="writer">The <see cref="BinaryWriter"/> to which the binary representation is written.</param>
        /// <remarks>
        /// The binary writer is not closed after this method completes.
        /// </remarks>
        public abstract void Assemble(BinaryWriter writer);
    }
}
